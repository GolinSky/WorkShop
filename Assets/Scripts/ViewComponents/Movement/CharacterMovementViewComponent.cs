using UnityEngine;
using WorkShop.Models.TransformModels;

namespace WorkShop.ViewComponents.Movement
{
    public class CharacterMovementViewComponent : BaseMovementViewComponent<CharacterController, ITransformModelObserver>
    {
        public override bool IsGrounded =>
            target.isGrounded; // Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
        //  QueryTriggerInteraction.Ignore);

        public override Vector3 Velocity => target.velocity;
        
        protected override void ChangePosition(Vector3 position)
        {
           
        }

        protected override void ChangeDirection(Vector3 direction)
        {
            target.transform.rotation = Model.Rotation;
            target.Move(direction);
        }
    }
}