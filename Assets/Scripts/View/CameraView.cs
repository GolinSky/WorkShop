using System;
using UnityEngine;
using WorkShop.Models.Camera;
using WorkShop.LightWeightFramework.Views;

namespace WorkShop.View
{
    public class CameraView : View<ICameraModelObserver>
    {
        protected override void OnInit(ICameraModelObserver model)
        {
            
            Model.OnPositionChanged += ModelOnOnPositionChanged;
        }

        protected override void OnRelease()
        {
            Model.OnPositionChanged += ModelOnOnPositionChanged;
        }

        private void ModelOnOnPositionChanged(Vector3 position)
        {
            transform.position = position;
            transform.LookAt(Model.LookAtPosition);
        }
    }
}