using UnityEngine;
using WorkShop.Models.TransformModels;

namespace WorkShop.ViewComponents.Movement
{
    public class CharacterMovementViewComponent : BaseMovementViewComponent<CharacterController, ITransformModelObserverObserver>
    {
        private GameObject MainCamera => Camera.main.gameObject;

        // player
        private float targetRotation = 0.0f;
        private float rotationVelocity;

        public override bool IsGrounded =>
            target.isGrounded; // Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
        //  QueryTriggerInteraction.Ignore);

        public override Vector3 Velocity => target.velocity;

        

        protected override void ChangePosition(Vector3 position)
        {
            Vector3 inputDirection = new Vector3(Model.MoveDirection.x, 0.0f, Model.MoveDirection.y).normalized;

        
            if (Model.MoveDirection != Vector2.zero)
            {
                targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                                  MainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity,
                    Model.RotationSmoothTime);

                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }


            Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

            target.Move(targetDirection.normalized * (Model.Speed * Time.deltaTime) +
                        new Vector3(0.0f, Model.VerticalVelocity, 0.0f) * Time.deltaTime);

        }
    }
}