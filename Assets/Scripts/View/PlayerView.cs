using UnityEngine;
using WorkShop.Commands.Player;
using WorkShop.LightWeightFramework.Views;
using WorkShop.Models;
using WorkShop.Models.TransformModels;
using WorkShop.MonoProviders;

namespace WorkShop.View
{
    public class PlayerView : View<IPlayerModelObserver, IPlayerCommand>
    {
        private IMovementProvider movementProvider;

        protected override void OnInit(IPlayerModelObserver model)
        {
            Model.GetModelObserver<ITransformModelObserver>().OnPositionChanged += OnChangePosition;
            foreach (var viewComponent in viewComponents)
            {
                if (viewComponent is IMovementProvider groundedProvider)
                {
                    this.movementProvider = groundedProvider;
                    break;
                }
            }
        }

        protected override void OnRelease()
        {
            Model.GetModelObserver<ITransformModelObserver>().OnPositionChanged -= OnChangePosition;
        }
        
        private void ChangeRotation(float yAxis)
        {
            //transform.Rotate(Vector3.up*yAxis);
            var angles = transform.eulerAngles;
            angles.y += yAxis;
            transform.eulerAngles = angles;
        }

        private void OnChangePosition(Vector3 position)
        {
            Command?.UpdatePosition(transform.position);
        }

        protected override void OnCommandSet(IPlayerCommand command)
        {
            command.RegisterMonoProvider(movementProvider);
        }
    }
}