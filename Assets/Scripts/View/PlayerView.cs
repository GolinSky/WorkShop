using UnityEngine;
using WorkShop.LightWeightFramework.Views;
using WorkShop.Models;

namespace WorkShop.View
{
    public class PlayerView:View<IPlayerModelObserver>
    {
        protected override void OnInit(IPlayerModelObserver model)
        {
            UpdatePosition(model.Position);
            model.OnPositionChanged += UpdatePosition;
        }

        private void UpdatePosition(Vector3 position)
        {
            transform.position = position;
        }

        protected override void OnRelease()
        {
            
        }
    }
}