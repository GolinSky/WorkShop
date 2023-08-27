using LightWeightFramework.Controller;
using UnityEngine;
using WorkShop.Commands;
using WorkShop.LightWeightFramework.Command;
using WorkShop.LightWeightFramework.UpdateService;
using WorkShop.Models.Input;

namespace WorkShop.Controllers.Input
{
    public class InputController: Controller<InputModel>, ITick
    {
        private bool resetInteract;
        public override string Id => "Input";

        public InputController(InputModel model) : base(model)
        {
        }

        public void MoveInput(Vector2 newMoveDirection)
        {
            Model.Move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            if (Model.CursorInputForLook)
            {
                Model.Look = newLookDirection;
            }
        }

        public void JumpInput(bool newJumpState)
        {
            Model.Jump = newJumpState;
        }

        public void SprintInput(bool newSprintState)
        {
            Model.Sprint = newSprintState;
        }
        
        public void InteractInput(bool interactState)
        {
            Model.Interact = interactState;
        }
        
        public override ICommand ConstructCommand()
        {
            return new InputCommand(this, GameObserver);
        }

        public void Notify(float state)
        {
            //if not fix this - use old input system instead
            if (resetInteract)
            {
                Model.Interact = false;
                resetInteract = false;
            }
            
            if (Model.Interact)
            {
                resetInteract = true;
            }

        }
    }
}