using UnityEngine;
using WorkShop.Commands.Player;
using WorkShop.LightWeightFramework.Views;
using WorkShop.Models;

namespace WorkShop.View
{
    public class PlayerView : View<IPlayerModelObserver, IPlayerCommand>
    {
        protected override void OnInit(IPlayerModelObserver model)
        {
            Model.OnPositionChanged += OnChangePosition;
        }
        
        protected override void OnRelease()
        {
            Model.OnPositionChanged -= OnChangePosition;
        }

        private void OnChangePosition(Vector3 position)
        {
            Command.UpdatePosition(transform.position);
        }
    }
}