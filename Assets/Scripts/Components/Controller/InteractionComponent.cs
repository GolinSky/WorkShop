using UnityEngine;
using WorkShop.LightWeightFramework.Game;
using WorkShop.Models.Input;
using WorkShop.Services.Interaction;
using WorkShop.Services.Player;
using WorkShop.Strategy;
using Component = WorkShop.LightWeightFramework.Components.Component;

namespace WorkShop.Components.Controller
{
    public interface IInteractionStrategy:IStrategy
    {
        void DoInteraction();
    }

    public class ThirdPersonInteractionStrategy:IInteractionStrategy
    {
        private readonly IInteractionService interactionService;

        public ThirdPersonInteractionStrategy(IInteractionService interactionService)
        {
            this.interactionService = interactionService;
        }
        public void DoInteraction()
        {
            if ( interactionService.HasInteraction(out var interactable))
            {
                interactable.TryInteract();
            }
        }
    }

    public class VehicleInteractionStrategy:IInteractionStrategy
    {
        private readonly IPlayerControlService playerControlService;

        public VehicleInteractionStrategy(IPlayerControlService playerControlService)
        {
            this.playerControlService = playerControlService;
        }
        
        public void DoInteraction()
        {
            playerControlService.SwitchState(PlayerControlState.ThirdPerson);
        }
    }
    
    public class InteractionComponent:Component, IStrategyContext<IInteractionStrategy>
    {
        private const float Delay = 0.1f;
        private readonly IInputModelObserver inputModelObserver;
        private IInteractionStrategy interactionStrategy;
        private float lastInteractionTime;
        public InteractionComponent(IInputModelObserver inputModelObserver)
        {
            this.inputModelObserver = inputModelObserver;
        }
        protected override void OnInit(IGameObserver gameObserver)
        {
            
        }

        protected override void OnRelease()
        {
        }

        public void SetStrategy(IInteractionStrategy interactionStrategy)
        {
            this.interactionStrategy = interactionStrategy;
        }

        public void Update(float deltaTime)
        {
            if (inputModelObserver.Interact && lastInteractionTime < Time.time)
            {
                lastInteractionTime = Time.time + Delay;
                interactionStrategy.DoInteraction();
            }
        }
    }
}