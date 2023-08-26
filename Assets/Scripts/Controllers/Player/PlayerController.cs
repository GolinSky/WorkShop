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
using WorkShop.Services.Player;
using WorkShop.Strategy;

namespace WorkShop.Controllers
{
    public class PlayerController: Controller<PlayerModel>, ITick
    {
        private IPlayerControlService playerControlService;
        private IInputModelObserver inputModel;
        private PlayerMoveComponent playerMoveComponent;
        private AnimationComponent animationComponent;
        private IMovementStrategy thirdPersonMovementStrategy;
        private IMovementStrategy defaultMovementStrategy;
        private Vector3 direction;
        private float currentY;

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
            thirdPersonMovementStrategy = AddComponent(new MoveComponent(Model.GetModel<ITransformModel>()));
        }

        protected override void OnInit()
        {
            base.OnInit();
            defaultMovementStrategy = new DefaultMovementStrategy();
            playerControlService = GetService<IPlayerControlService>();
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
            switch (controlState)
            {
                case PlayerControlState.ThirdPerson:
                    playerMoveComponent.SetStrategy(thirdPersonMovementStrategy);
                    break;
                case PlayerControlState.AirCraft:
                    playerMoveComponent.SetStrategy(defaultMovementStrategy);
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
        }
        
        public override ICommand ConstructCommand()
        {
            return new PlayerCommand(this, GameObserver);
        }
    }
}