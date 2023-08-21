using UnityEngine;
using WorkShop.LightWeightFramework.ViewComponents;
using WorkShop.Models.TransformModels;
using WorkShop.MonoProviders;

namespace WorkShop.ViewComponents.Movement
{
    public abstract class BaseMovementViewComponent<TComponent>:ViewComponent<ITransformModelObserver>, IMovementProvider
        where TComponent:Component
    {
        [SerializeField] protected TComponent target;
     
        public abstract Vector3 Velocity { get; }
        public abstract bool IsGrounded { get; }


        protected override void OnInit()
        {
            base.OnInit();
            Model.OnPositionChanged += ChangePosition;
        }
        
        protected override void OnRelease()
        {
            Model.OnPositionChanged -= ChangePosition;
        }

        protected abstract void ChangePosition(Vector3 position);
    }
}