using UnityEngine;
using WorkShop.LightWeightFramework.ViewComponents;
using WorkShop.Models.TransformModels;

namespace WorkShop.ViewComponents.Movement
{
    public abstract class BaseMovementViewComponent<TComponent>:ViewComponent<ITransformModelObserver>
        where TComponent:Component
    {
        [SerializeField] protected TComponent target;
        
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