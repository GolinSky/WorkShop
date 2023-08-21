using LightWeightFramework.Controller;
using UnityEngine;
using WorkShop.Commands;
using WorkShop.LightWeightFramework.Command;
using WorkShop.Models.Input;
using WorkShop.Services.Player;

namespace WorkShop.Controllers.Input
{
    public class InputController: Controller<InputModel>
    {
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
        
        public override ICommand GetCommand()
        {
            return new InputCommand(this, GameObserver);
        }
    }
}