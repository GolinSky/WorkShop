using UnityEngine;
using WorkShop.Models.TransformModels;

namespace WorkShop.ViewComponents.Movement
{
    public class MovementViewComponent:BaseMovementViewComponent<Transform, IBaseTransformModelObserver>
    {
        public override bool IsGrounded => true;
        public override Vector3 Velocity { get; }

        protected override void ChangePosition(Vector3 position)
        {
            target.position = position;
        }
    }
}