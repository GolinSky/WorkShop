using UnityEngine;

namespace WorkShop.Views
{
    //temp solution
    public class AirCraftMonoBehaviour:MonoBehaviour
    {
        private const float PowerValue = 20f;
        [SerializeField] private Rigidbody rigidbody;

        private Vector3 anglesVelocity;
        private Vector3 velocity;

        private float pitch;
        private float yaw;
        private float roll;
        private float power;
        private bool canExecute;
        

        public void UpdateState(float pitch, float yaw, float roll, float power = PowerValue)
        {
            this.pitch = pitch;
            this.yaw = yaw;
            this.roll = roll;
            this.power = power;
        }

        public void UpdateState(bool canExecute)
        {
            this.canExecute = canExecute;
        }

        private void FixedUpdate()
        {
            if(!canExecute) return;

           rigidbody.MovePosition(transform.position + (transform.forward * Time.fixedDeltaTime * power));

           Quaternion turnRotation = Quaternion.Euler(pitch, yaw, -roll);
           rigidbody.MoveRotation(rigidbody.rotation * turnRotation);
        }
    }
}