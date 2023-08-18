using UnityEngine;
using WorkShop.Commands.Player;
using WorkShop.LightWeightFramework.Views;
using WorkShop.Models;
using WorkShop.MonoProviders;

namespace WorkShop.View
{
    public class PlayerView : View<IPlayerModelObserver, IPlayerCommand>
    {
        private IGroundedProvider groundedProvider;

        protected override void OnInit(IPlayerModelObserver model)
        {
            Model.OnPositionChanged += OnChangePosition;
            foreach (var viewComponent in viewComponents)
            {
                if (viewComponent is IGroundedProvider groundedProvider)
                {
                    this.groundedProvider = groundedProvider;
                    break;
                }
            }
        }
        
        protected override void OnRelease()
        {
            Model.OnPositionChanged -= OnChangePosition;
        }

        private void OnChangePosition(Vector3 position)
        {
            Command?.UpdatePosition(transform.position);
        }

        protected override void OnCommandSet(IPlayerCommand command)
        {
            command.RegisterMonoProvider(groundedProvider);
        }
    }
}