using UnityEngine;

namespace WorkShop.ViewComponents.Movement
{
    public class MovementViewComponent:BaseMovementViewComponent<Transform>
    {
        protected override void ChangePosition(Vector3 position)
        {
            target.position = position;
        }
    }
}