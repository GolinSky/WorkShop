using System.Collections.Generic;
using LightWeightFramework.Controller;
using UnityEngine;
using WorkShop.Commands.Player;
using WorkShop.Components.Controller;
using WorkShop.LightWeightFramework.Command;
using WorkShop.LightWeightFramework.Components;
using WorkShop.LightWeightFramework.UpdateService;
using WorkShop.Models;
using WorkShop.Models.Input;
using WorkShop.Models.TransformModels;
using WorkShop.MonoProviders;

namespace WorkShop.Controllers
{
    public class PlayerController: Controller<PlayerModel>, ITick
    {
        private PlayerMoveComponent moveComponent;
        private AnimationComponent animationComponent;
        private IMovementProvider movementProvider;
        private IInputModelObserver inputModel;
        private ITransformModel currentTransformModel;
        private Vector3 direction;
        private float currentY;

        public override string Id => "Player";

        public PlayerController(PlayerModel model) : base(model)
        {
            currentTransformModel = Model.GetModel<ITransformModel>();
        }

        protected override void OnBeforeComponentsInitialed()
        {
            base.OnBeforeComponentsInitialed();
            inputModel = GameObserver.ModelHub.GetModel<IInputModelObserver>();
        }

        protected override void OnInit()
        {
            base.OnInit();
            moveComponent = GetComponent<PlayerMoveComponent>();
            animationComponent = GetComponent<AnimationComponent>();
        }

        protected override List<IComponent> BuildsComponents()
        {
            var moveComponent = new MoveComponent(Model.TransformModel);
            var components = base.BuildsComponents();
            components.Add(new UpdateComponent(this));
            components.Add(moveComponent);
            components.Add(new AnimationComponent(Model, inputModel));
            components.Add(new PlayerMoveComponent(Model, moveComponent, inputModel));
            return components;
        }
        
        
        public void Notify(float deltaTime)
        {
            if(inputModel == null) return;
            
            //move to command
            currentTransformModel.Grounded = movementProvider.IsGrounded;
            currentTransformModel.Velocity = movementProvider.Velocity;

            moveComponent.Update(deltaTime);
            animationComponent.Update(deltaTime);
        }
        
        public override ICommand GetCommand()
        {
            return new PlayerCommand(this, GameObserver);
        }

        public void RegisterGroundedProvider(IMovementProvider movementProvider)
        {
            this.movementProvider = movementProvider;
        }
    }
}