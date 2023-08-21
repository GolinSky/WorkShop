using UnityEngine;
using WorkShop.Controllers.Input;
using WorkShop.LightWeightFramework.Command;
using WorkShop.LightWeightFramework.Game;

namespace WorkShop.Commands
{
    public interface IInputCommand : ICommand
    {
        void MoveInput(Vector2 newMoveDirection);
        void LookInput(Vector2 newLookDirection);
        void JumpInput(bool newJumpState);
        void SprintInput(bool newSprintState);
    }

    public class InputCommand : Command<InputController>, IInputCommand
    {
        public InputCommand(InputController controller, IGameObserver gameObserver) : base(controller, gameObserver)
        {
        }

        public void MoveInput(Vector2 newMoveDirection)
        {
            Controller.MoveInput(newMoveDirection);
        }

        public void LookInput(Vector2 newLookDirection)
        {
            Controller.LookInput(newLookDirection);
        }

        public void JumpInput(bool newJumpState)
        {
            Controller.JumpInput(newJumpState);
        }

        public void SprintInput(bool newSprintState)
        {
            Controller.SprintInput(newSprintState);
        }
    }
}