using UnityEngine;
using WorkShop.Commands.Player;
using WorkShop.LightWeightFramework.Command;
using WorkShop.LightWeightFramework.ViewComponents;
using WorkShop.Models.TransformModels;
using WorkShop.MonoProviders;

namespace WorkShop.ViewComponents.Movement
{
    public abstract class BaseMovementViewComponent<TComponent, TTransformModel>:ViewComponent<TTransformModel>, IMovementProvider, ICommandInvoker
        where TComponent:Component
        where TTransformModel:IBaseTransformModelObserver
    {
        [SerializeField] protected TComponent target;
        protected IPlayerCommand PlayerCommand { get; private set; }

        public abstract Vector3 Velocity { get; }
        public virtual Vector3 Position => transform.position;
        public Vector3 Angles => transform.eulerAngles;
        public abstract bool IsGrounded { get; }


        protected override void OnInit()
        {
            base.OnInit();
            Model.OnPositionChanged += ChangePosition;
            Model.OnDirectionChanged += ChangeDirection;
        }
        
        protected override void OnRelease()
        {
            Model.OnPositionChanged -= ChangePosition;
            Model.OnDirectionChanged -= ChangeDirection;
        }

        protected abstract void ChangePosition(Vector3 position);
        protected abstract void ChangeDirection(Vector3 direction);
        public void SetCommand(ICommand command)
        {
            PlayerCommand = (IPlayerCommand)command;
            PlayerCommand.ActorTransformCommand.RegisterMonoProvider(this);
        }
    }
}