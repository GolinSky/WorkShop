using UnityEngine;
using WorkShop.LightWeightFramework.Game;
using WorkShop.Models;
using WorkShop.Models.Input;
using WorkShop.Models.TransformModels;
using Component = WorkShop.LightWeightFramework.Components.Component;

namespace WorkShop.Components.Controller
{
    public class PlayerMoveComponent : Component
    {
        private const float ZeroSpeed = 0.0f;
        private const float DefaultMagnitude = 1.0f;
        private const float TerminalVelocity = 53.0f; //what a magic number from unity default assets
        private const float SpeedOffset = 0.1f;

        private readonly ITransformModel model;
        private readonly IMoveComponent moveComponent;
        private readonly IInputModelObserver inputModel;

        private float verticalVelocity;
        private float jumpTimeoutDelta;
        private float targetRotation;

        public PlayerMoveComponent(PlayerModel model, IMoveComponent moveComponent, IInputModelObserver inputModel)
        {
            this.model = model.GetModel<ITransformModel>();
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
                if (verticalVelocity < 0.0f)
                {
                    verticalVelocity = -2f;
                }

                if (inputModel.Jump && jumpTimeoutDelta <= 0.0f)
                {
                    verticalVelocity = Mathf.Sqrt(model.JumpHeight * -2f * model.Gravity);
                    model.InvokeJumpEvent();
                }

                if (jumpTimeoutDelta >= 0.0f)
                {
                    jumpTimeoutDelta -= deltaTime;
                }
            }
            else
            {
                jumpTimeoutDelta = model.JumpTimeout;
                inputModel.Jump = false; //move this reset out of here
            }

            if (verticalVelocity < TerminalVelocity)
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

            float inputMagnitude = inputModel.AnalogMovement
                ? inputModel.Move.magnitude
                : DefaultMagnitude;

            if (currentHorizontalSpeed < targetSpeed - SpeedOffset ||
                currentHorizontalSpeed > targetSpeed + SpeedOffset)
            {
                model.Speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                    deltaTime * model.SpeedChangeRate);

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

            moveComponent.Move(deltaTime, model.MoveDirection);
        }
    }
}