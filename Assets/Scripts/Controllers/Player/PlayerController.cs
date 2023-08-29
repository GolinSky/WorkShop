using System;
using LightWeightFramework.Controller;
using UnityEngine;
using WorkShop.Commands.Player;
using WorkShop.Components.Controller;
using WorkShop.LightWeightFramework.Command;
using WorkShop.LightWeightFramework.UpdateService;
using WorkShop.Models;
using WorkShop.Models.Input;
using WorkShop.Models.TransformModels;
using WorkShop.Services.Interaction;
using WorkShop.Services.Player;
using WorkShop.Strategy;

namespace WorkShop.Controllers
{
    public class PlayerController : Controller<PlayerModel>, ITick
    {
        private IPlayerControlService playerControlService;
        private IVehicleTransformService vehicleTransformService;
        private IInteractionService interactionService;

        private IInputModelObserver inputModel;

        private PlayerMoveComponent playerMoveComponent;
        private AnimationComponent animationComponent;
        private InteractionComponent interactionComponent;
        
        private IMovementStrategy thirdPersonMovementStrategy;
        private IMovementStrategy defaultMovementStrategy;
        private IInteractionStrategy thirdPersonInteractionStrategy;
        private IInteractionStrategy vehicleInteractionStrategy;
        
        public override string Id => "Player";

        public PlayerController(PlayerModel model) : base(model)
        {
        }

        protected override void OnBeforeComponentsInitialed()
        {
            base.OnBeforeComponentsInitialed();
            inputModel = GameObserver.ModelHub.GetModel<IInputModelObserver>();
            playerMoveComponent = AddComponent(new PlayerMoveComponent(Model, inputModel));
            animationComponent = AddComponent(new AnimationComponent(Model, inputModel));
            interactionComponent = AddComponent(new InteractionComponent(inputModel));
            thirdPersonMovementStrategy = AddComponent(new MoveComponent(Model.GetModel<ITransformModel>()));
        }

        protected override void OnInit()
        {
            base.OnInit();
            defaultMovementStrategy = new DefaultMovementStrategy();

            playerControlService = GetService<IPlayerControlService>();
            vehicleTransformService = GetService<IVehicleTransformService>();
            interactionService = GetService<IInteractionService>();
            thirdPersonInteractionStrategy = new ThirdPersonInteractionStrategy(interactionService);
            vehicleInteractionStrategy = new VehicleInteractionStrategy(playerControlService);
            OnControlStateChanged(playerControlService.CurrentState);
            playerControlService.OnControlStateChanged += OnControlStateChanged;
        }

        protected override void OnRelease()
        {
            base.OnRelease();
            playerControlService.OnControlStateChanged -= OnControlStateChanged;
        }

        private void OnControlStateChanged(PlayerControlState controlState)
        {
            Model.ControlState = controlState;
            switch (controlState)
            {
                case PlayerControlState.ThirdPerson:
                    playerMoveComponent.SetStrategy(thirdPersonMovementStrategy);
                    animationComponent.SetBlock(false);
                    playerMoveComponent.SetParent(null);
                    interactionComponent.SetStrategy(thirdPersonInteractionStrategy);
                    break;
                case PlayerControlState.AirCraft:
                    var vehicleTransform = vehicleTransformService.CurrentVehicleTransform;
                    if (vehicleTransform != null)
                    {
                        playerMoveComponent.SetParent(vehicleTransform);
                    }
                    playerMoveComponent.SetStrategy(defaultMovementStrategy);
                    animationComponent.SetBlock(true);
                    interactionComponent.SetStrategy(vehicleInteractionStrategy);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(controlState), controlState, null);
            }
        }

        public void Notify(float deltaTime)
        {
            if(inputModel == null) return;
            
            playerMoveComponent.Update(deltaTime);
            animationComponent.Update(deltaTime);
            interactionComponent.Update(deltaTime);
        }
        
        public override ICommand ConstructCommand()
        {
            return new PlayerCommand(this, GameObserver);
        }
    }
}