using System;
using GofPatterns.Patterns.Behavioral.Observer.Custom;
using UnityEngine;
using WorkShop.LightWeightFramework.Game;
using WorkShop.LightWeightFramework.Service;

namespace WorkShop.Services.Interaction
{
    public interface IInteractionService:IService
    {
        event Action<bool> OnInteractionChanged; 
        void EnableInteractionState();
        bool HasInteraction { get; }
    }
    
    public class InteractionService: Service, IInteractionService, ICustomObserver<float>
    {
        private const float ResetDelay = 1f;
        
        public event Action<bool> OnInteractionChanged;
        
        private ITickService tickService;
        private float resetTime;
        public bool HasInteraction { get; private set; }
        
        protected override void OnInit(IGameObserver gameObserver)
        {
            tickService = gameObserver.ServiceHub.Get<ITickService>();
            tickService.AddObserver(this);
        }

        protected override void Release()
        {
            tickService.RemoveObserver(this);
        }
        
        public void EnableInteractionState()
        {
            resetTime = Time.time + ResetDelay;
            UpdateInteraction(true);
        }
        
        public void Notify(float state)
        {
            if (HasInteraction)
            {
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