using UnityEngine;
using WorkShop.Commands.Camera;
using WorkShop.Models.Camera;
using WorkShop.LightWeightFramework.Views;
using WorkShop.MonoProviders;

namespace WorkShop.View
{
    public class CameraView : View<ICameraModelObserver, ICameraCommand>, IMovementProvider
    {
        public bool IsGrounded => false;
        public Vector3 Velocity => Vector3.zero;
        public Vector3 Position => transform.position;
        public Vector3 Angles => transform.eulerAngles;
        protected override void OnInit(ICameraModelObserver model)
        {
            Model.OnPositionChanged += ModelOnOnPositionChanged;
        }

        protected override void OnRelease()
        {
            Model.OnPositionChanged -= ModelOnOnPositionChanged;
        }

        private void ModelOnOnPositionChanged(Vector3 position)
        {
            transform.position = position;
            transform.LookAt(Model.LookAtPosition);
        }

        protected override void OnCommandSet(ICameraCommand command)
        {
            command.ActorTransformCommand.RegisterMonoProvider(this);
        }
    }
}