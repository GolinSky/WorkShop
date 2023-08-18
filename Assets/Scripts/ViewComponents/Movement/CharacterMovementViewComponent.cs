using UnityEngine;

namespace WorkShop.ViewComponents.Movement
{
    public class CharacterMovementViewComponent:BaseMovementViewComponent<CharacterController>
    {
        public override bool IsGrounded => target.isGrounded;

        protected override void OnInit()
        {
            base.OnInit();
            target.transform.position = Model.Position;
        }

        protected override void ChangePosition(Vector3 position)
        {
            target.Move(Model.Direction * Model.Speed * Time.deltaTime);
        }
    }
}