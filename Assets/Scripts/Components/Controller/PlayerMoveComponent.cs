using UnityEngine;
using WorkShop.LightWeightFramework.Game;
using WorkShop.Models.Input;
using WorkShop.Models.TransformModels;
using Component = WorkShop.LightWeightFramework.Components.Component;

namespace WorkShop.Components.Controller
{
    public class PlayerMoveComponent:Component
    {
        private const float ZeroSpeed = 0.0f;
        private const float DefaultMagnitude = 1.0f;
        
        private readonly ITransformModel model;
        private readonly IMoveComponent moveComponent;
        private readonly IInputModelObserver inputModel;

        private float terminalVelocity = 53.0f;//what a magic number from unity default assets
        private float verticalVelocity;
        private float jumpTimeoutDelta;
        private float targetRotation;

        public PlayerMoveComponent(ITransformModel model, IMoveComponent moveComponent, IInputModelObserver inputModel)
        {
            this.model = model;
            this.moveComponent = moveComponent;
            this.inputModel = inputModel;
        }
        
        protected override void OnInit(IGameObserver gameObserver)
        {
            jumpTimeoutDelta = model.JumpTimeout;

        }

        protected override void OnRelease()
        {
            
        }

        public void Update(float deltaTime)
        {
            if (model.Grounded)
            {
                // stop our velocity dropping infinitely when grounded
                if (verticalVelocity < 0.0f)
                {
                    verticalVelocity = -2f;
                }

                // Jump
                if (inputModel.Jump && jumpTimeoutDelta <= 0.0f)
                {
                    // the square root of H * -2 * G = how much velocity needed to reach desired height
                    verticalVelocity = Mathf.Sqrt(model.JumpHeight * -2f * model.Gravity);
                    model.InvokeJumpEvent();
                }

                // jump timeout
                if (jumpTimeoutDelta >= 0.0f)
                {
                    jumpTimeoutDelta -= deltaTime;
                }
            }
            else
            {
                jumpTimeoutDelta = model.JumpTimeout;
                inputModel.Jump = false;//move this reset out of here

            }
            
            if (verticalVelocity < terminalVelocity)
            {
                verticalVelocity += model.Gravity * deltaTime;
            }

            model.VerticalVelocity = verticalVelocity;
            
            float targetSpeed = inputModel.Move == Vector2.zero
                ? ZeroSpeed
                : inputModel.Sprint
                    ? model.SprintSpeed
                    : model.MoveSpeed;

            float currentHorizontalSpeed = new Vector3(model.Velocity.x, 0.0f, model.Velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = inputModel.AnalogMovement ? inputModel.Move.magnitude : 1f;

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                model.Speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                    Time.deltaTime * model.SpeedChangeRate);

                // round speed to 3 decimal places
                model.Speed = Mathf.Round(model.Speed * 1000f) / 1000f;
            }
            else
            {
                model.Speed = targetSpeed;
            }
            
           
            model.InputMagnitude = inputModel.AnalogMovement 
                ? inputModel.Move.magnitude 
                : DefaultMagnitude;

            model.MoveDirection = inputModel.Move;
            Debug.Log(model.ToString());
          //  Vector3 inputDirection = new Vector3(model.MoveDirection.x, 0.0f, model.MoveDirection.y).normalized;

          moveComponent.Move(deltaTime, model.MoveDirection);
        }
    }
}