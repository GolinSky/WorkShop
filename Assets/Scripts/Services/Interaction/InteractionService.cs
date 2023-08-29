using System;
using GofPatterns.Patterns.Behavioral.Observer.Custom;
using UnityEngine;
using WorkShop.LightWeightFramework.Game;
using WorkShop.LightWeightFramework.Service;
using WorkShop.LightWeightFramework.UpdateService;
using WorkShop.ViewComponents;

namespace WorkShop.Services.Interaction
{
    public interface IInteractionService:IService
    {
        event Action<bool> OnInteractionChanged; 
        void AddInteractable(IInteractable interactable);
        bool HasInteraction(out IInteractable interactable);
    }
    
    public class InteractionService: Service, IInteractionService, ITick
    {
        private const float ResetDelay = 1f;
        public event Action<bool> OnInteractionChanged;
        
        private ITickService tickService;
        private float resetTime;
        private IInteractable interactable;

        public bool HasInteraction { get; private set; }
        
        public void AddInteractable(IInteractable interactable)
        {
            resetTime = Time.time + ResetDelay;
            UpdateInteraction(true);
            this.interactable = interactable;
        }

        bool IInteractionService.HasInteraction(out IInteractable interactable)
        {
            interactable = null;
            if (HasInteraction)
            {
                interactable = this.interactable;
                return interactable != null;
            }

            return false;
        }

        protected override void OnInit(IGameObserver gameObserver)
        {
            tickService = gameObserver.ServiceHub.Get<ITickService>();
            tickService.AddObserver(this);
        }

        protected override void Release()
        {
            tickService.RemoveObserver(this);
        }
        
        public void Notify(float state)
        {
            if (HasInteraction)
            {
                //check input
                // apply interaction
                if (resetTime < Time.time)
                {
                    UpdateInteraction(false);
                }
            }
        }

        private void UpdateInteraction(bool state)
        {
            HasInteraction = state;
            OnInteractionChanged?.Invoke(HasInteraction);
        }
    }
}