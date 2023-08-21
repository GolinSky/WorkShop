using StarterAssets;
using UnityEngine;
using UnityEngine.Serialization;

namespace WorkShop.ViewComponents.Movement
{
    public class CharacterMovementViewComponent : BaseMovementViewComponent<CharacterController>
    {
     
        
        private GameObject MainCamera => Camera.main.gameObject;

        // player
        private float animationBlend;
        private float targetRotation = 0.0f;
        private float rotationVelocity;
     //   private float speed;

        public override bool IsGrounded =>
            target.isGrounded; // Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,

        public override Vector3 Velocity => target.velocity;
        //  QueryTriggerInteraction.Ignore);


        protected override void OnInit()
        {
            base.OnInit();
            // reset our timeouts on start
            // if (_mainCamera == null)
            // {
            //     _mainCamera = FindObjectOfType<Camera>().gameObject;// GameObject.FindGameObjectWithTag("MainCamera");
            // }
        }

        protected override void ChangePosition(Vector3 position)
        {

            // normalise input direction
            Vector3 inputDirection = new Vector3(Model.MoveDirection.x, 0.0f, Model.MoveDirection.y).normalized;

            // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is a move input rotate player when the player is moving
            if (Model.MoveDirection != Vector2.zero)
            {
                targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                                  MainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity,
                    Model.RotationSmoothTime);

                // rotate to face input direction relative to camera position
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }


            Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

            // move the player
            target.Move(targetDirection.normalized * (Model.Speed * Time.deltaTime) +
                        new Vector3(0.0f, Model.VerticalVelocity, 0.0f) * Time.deltaTime);

            // target.Move(Model.Direction * Model.Speed * Time.deltaTime);
        }
    }
}