using LightWeightFramework.Model;
using UnityEngine;
using WorkShop.LightWeightFramework.Command;
using WorkShop.LightWeightFramework.ViewComponents;

namespace WorkShop.LightWeightFramework.Views
{
    public abstract class View : MonoBehaviour, IView
    {
        [SerializeField] protected ViewComponent[] viewComponents;
        public IModelObserver ModelObserver { get; private set; }

        public virtual void Init(IModelObserver model)
        {
            ModelObserver = model;
            for (var i = 0; i < viewComponents.Length; i++)
            {
                viewComponents[i].Init(this);
            }
        }

        public virtual void Release()
        {
            for (var i = 0; i < viewComponents.Length; i++)
            {
                viewComponents[i].Release();
            }
        }
    }

    public abstract class View<TModel> : View
        where TModel:IModelObserver
    {
        protected TModel Model { get; private set; }

        public sealed override void Init(IModelObserver model)
        {
            base.Init(model);
            Model = (TModel)model;
            OnInit(Model);
        }

        public sealed override void Release()
        {
            base.Release();
            OnRelease();
        }

        protected abstract void OnInit(TModel model);
        protected abstract void OnRelease();
    }

    public abstract class View<TModel, TCommand> : View<TModel>, ICommandInvoker
        where TModel : IModelObserver 
        where TCommand : ICommand
    {
        protected TCommand Command { get; private set; }
        
        void ICommandInvoker.SetCommand(ICommand command)
        {
            Command = (TCommand)command;
        }
    }
}