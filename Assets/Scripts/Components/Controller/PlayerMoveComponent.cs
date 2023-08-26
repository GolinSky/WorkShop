using GofPatterns.Patterns.Behavioral;
using UnityEngine;
using WorkShop.LightWeightFramework.Game;
using WorkShop.Models;
using WorkShop.Models.Input;
using WorkShop.Models.TransformModels;
using WorkShop.MonoProviders;
using WorkShop.Services.Player;
using WorkShop.Strategy;
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
        private readonly IInputModelObserver inputModel;
        private IMovementProvider playerProvider;
        private IMovementProvider cameraProvider;
        private IActorTransformService actorTransformService;
        private IMovementStrategy movementStrategy;

        private float verticalVelocity;
        private float jumpTimeoutDelta;
        private float targetRotation;
        private float rotationVelocity;
        private float speed;

        public PlayerMoveComponent(PlayerModel model, IInputModelObserver inputModel)
        {
            movementStrategy = new DefaultMovementStrategy();
            this.model = model.GetModel<ITransformModel>();
            this.inputModel = inputModel;
        }

        protected override void OnInit(IGameObserver gameObserver)
        {
            jumpTimeoutDelta = model.JumpTimeout;
            actorTransformService = GameObserver.ServiceHub.Get<IActorTransformService>();
            actorTransformService.HasActor(ActorType.Player, out playerProvider);//rebuild
            actorTransformService.HasActor(ActorType.Camera, out cameraProvider);//rebuild
            actorTransformService.OnActorAdded += UpdateActor;
        }

        protected override void OnRelease()
        {
            actorTransformService.OnActorAdded -= UpdateActor;
        }


        public void SetStrategy(IMovementStrategy strategy)
        {
            movementStrategy = strategy;
        }
        
        private void UpdateActor(ActorType actorType, IMovementProvider movementProvider)
        {
            switch (actorType)
            {
                case ActorType.Player:
                    playerProvider = movementProvider;
                    break;
                case ActorType.Camera:
                    cameraProvider = movementProvider;
                    break;
            }
        }

        public void Update(float deltaTime)
        {
            UpdateDataFromProviders();
            CalculateGravity(deltaTime);
            CalculateSpeed(deltaTime);
            var direction = CalculateDirection(deltaTime);
            movementStrategy.Move(deltaTime, direction);
        }

        private Vector3 CalculateDirection(float deltaTime)
        {
            var moveDirection = inputModel.Move;
            
            Vector3 inputDirection = new Vector3(moveDirection.x, 0.0f, moveDirection.y).normalized;

            if (moveDirection != Vector2.zero)
            {
                targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                                 cameraProvider.Angles.y;
                float rotation = Mathf.SmoothDampAngle(playerProvider.Angles.y, targetRotation, ref rotationVelocity,
                    model.RotationSmoothTime);

                model.Rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }

            Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;
            var direction = targetDirection.normalized * (speed * deltaTime) +
                            new Vector3(0.0f, verticalVelocity, 0.0f) * deltaTime;
            return direction;
        }

        private void CalculateSpeed(float deltaTime)
        {
            float targetSpeed = inputModel.Move == Vector2.zero
                ? ZeroSpeed
                : inputModel.Sprint
                    ? model.SprintSpeed
                    : model.MoveSpeed;

            float currentHorizontalSpeed = new Vector3(model.Velocity.x, 0.0f, model.Velocity.z).magnitude;

            model.InputMagnitude = inputModel.AnalogMovement
                ? inputModel.Move.magnitude
                : DefaultMagnitude;

            if (currentHorizontalSpeed < targetSpeed - SpeedOffset ||
                currentHorizontalSpeed > targetSpeed + SpeedOffset)
            {
                speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * model.InputMagnitude,
                    deltaTime * model.SpeedChangeRate);

                speed = Mathf.Round(speed * 1000f) / 1000f;
            }
            else
            {
                speed = targetSpeed;
            }
        }

        private void CalculateGravity(float deltaTime)
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
        }

        private void UpdateDataFromProviders()
        {
            if (playerProvider != null)
            {
                model.Grounded = playerProvider.IsGrounded;
                model.Velocity = playerProvider.Velocity;
            }
        }
    }
}