using UnityEngine;
using UnityEngine.InputSystem;
using WorkShop.Commands;
using WorkShop.Models.Input;
using WorkShop.LightWeightFramework;

namespace WorkShop.Views
{
    public class InputView : View<IInputModelObserver, IInputCommand>
    {
#if ENABLE_INPUT_SYSTEM
        public void OnMove(InputValue value)
        {
            Command.MoveInput(value.Get<Vector2>());
        }

        public void OnLook(InputValue value)
        {
            Command.LookInput(value.Get<Vector2>());
        }

        public void OnJump(InputValue value)
        {
            Command.JumpInput(value.isPressed);
        }

        public void OnSprint(InputValue value)
        {
            Command.SprintInput(value.isPressed);
        }

        public void OnInteract(InputValue value)
        {
            Command.InteractInput(value.isPressed);
        }
#endif
        
        private void SetCursorState(bool newState)// move from this to controller
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }

        protected override void OnInit(IInputModelObserver model)
        {
        }

        protected override void OnRelease()
        {
        }

        protected override void OnCommandSet(IInputCommand command)
        {
        }
    }
}