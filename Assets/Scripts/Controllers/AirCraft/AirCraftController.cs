using LightWeightFramework.Controller;
using UnityEngine;
using WorkShop.Commands.AirCraft;
using WorkShop.LightWeightFramework.Command;
using WorkShop.LightWeightFramework.UpdateService;
using WorkShop.Models.AirCraft;
using WorkShop.Models.Input;
using WorkShop.Models.TransformModels;
using WorkShop.Services.Interaction;
using WorkShop.Services.Player;
using WorkShop.ViewComponents;

namespace WorkShop.Controllers.AirCraft
{
    public class AirCraftController:Controller<AirCraftModel>, ITick, IInteractable
    {
        private IPlayerControlService playerControlService;
        private IInteractionService interactionService;
        private IVehicleTransformService vehicleTransformService;
        
        private IInputModelObserver inputModel;
        private ITransformModel transformModel;


        private Vector3 direction;
        private Transform playerSitTransform;


        public AirCraftController(AirCraftModel model) : base(model)
        {
            transformModel = Model.GetModel<ITransformModel>();
        }
        
        protected override void OnBeforeComponentsInitialed()
        {
            base.OnBeforeComponentsInitialed();
            inputModel = GameObserver.ModelHub.GetModel<IInputModelObserver>();
        }

        protected override void OnInit()
        {
            base.OnInit();
            playerControlService = GetService<IPlayerControlService>();
            interactionService = GetService<IInteractionService>();
            vehicleTransformService = GetService<IVehicleTransformService>();
            UpdateControlState(playerControlService.CurrentState);
            playerControlService.OnControlStateChanged += UpdateControlState;
        }

        protected override void OnRelease()
        {
            base.OnRelease();
            playerControlService.OnControlStateChanged -= UpdateControlState;
        }

        private void UpdateControlState(PlayerControlState controlState)
        {
            Model.ControlState = controlState;
        }

        public void Notify(float state)
        {
            if (playerControlService.CurrentState != PlayerControlState.AirCraft)
            {
                return;
            }
            
            if(inputModel == null) return;
            
            direction.x = ClampToOne(inputModel.Move.x);
            direction.z = ClampToOne(inputModel.Move.y);
            Model.Throttle = inputModel.Jump? 1 : 0;
            transformModel.UpdateDirection(direction);
        }
        
        public void TryInteract()
        {
            if (playerControlService.CurrentState == PlayerControlState.AirCraft) return;
            
            vehicleTransformService.UpdateCurrentVehicleTransform(playerSitTransform);
            playerControlService.SwitchState(PlayerControlState.AirCraft);
        }

        public void SetPlayerSitTransform(Transform playerSitTransform)
        {
            this.playerSitTransform = playerSitTransform;
        }
        
        public override ICommand ConstructCommand()
        {
            return new AirCraftCommand(this, GameObserver);
        }

        private float ClampToOne(float value)
        {
            return Mathf.Clamp(value, -1f, 1f);
        }
    }
}