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

namespace WorkShop.Controllers.AirCraft
{
    public class AirCraftController:Controller<AirCraftModel>, ITick
    {
        private IPlayerControlService playerControlService;
        private IInteractionService interactionService;
        private IVehicleTransformService vehicleTransformService;
        
        private IInputModelObserver inputModel;
        private ITransformModel transformModel;


        private Vector3 direction;
        
        
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
        }

        public void Notify(float state)
        {
            if (playerControlService.CurrentState != PlayerControlState.AirCraft)
            {
                return;
            }
            
            if(inputModel == null) return;

            direction.x = inputModel.Move.x;
            direction.z = inputModel.Move.y;
            direction.y = inputModel.Look.y + inputModel.Move.y/2.0f;
            transformModel.UpdateDirection(direction * transformModel.SprintSpeed);
        }
        
        public override ICommand ConstructCommand()
        {
            return new AirCraftCommand(this, GameObserver);
        }

        public bool TryInteract(Transform playerSitTransform)
        {
            if (playerControlService.CurrentState == PlayerControlState.AirCraft) return false;

            interactionService.EnableInteractionState();
            if (inputModel.Interact)
            {
                vehicleTransformService.UpdateCurrentVehicleTransform(playerSitTransform);
                playerControlService.SwitchState(PlayerControlState.AirCraft);
                return true;
            }

            return false;
        }
    }
}