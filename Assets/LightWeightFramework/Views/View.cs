using System.Collections.Generic;
using LightWeightFramework.Model;
using UnityEngine;
using WorkShop.LightWeightFramework.Command;
using WorkShop.LightWeightFramework.ViewComponents;

namespace WorkShop.LightWeightFramework
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
                InitViewComponent(viewComponents[i]);
            }
        }

        public virtual void Release()
        {
            for (var i = 0; i < viewComponents.Length; i++)
            {
                ReleaseViewComponent(viewComponents[i]);
            }
        }

        private void InitViewComponent(ViewComponent viewComponent)
        {
            viewComponent.Init(this);
            OnInitViewComponent(viewComponent);
        }

        private void ReleaseViewComponent(ViewComponent viewComponent)
        {
            viewComponent.Release();
            OnReleaseViewComponent(viewComponent);
        }

        protected virtual void OnInitViewComponent(ViewComponent viewComponent){}
        protected virtual void OnReleaseViewComponent(ViewComponent viewComponent){}
        
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
        private List<ICommandInvoker> commandInvokers = new List<ICommandInvoker>();
        
        void ICommandInvoker.SetCommand(ICommand command)
        {
            Command = (TCommand)command;
            OnCommandSet(Command);
            foreach (var commandInvoker in commandInvokers)
            {
                commandInvoker.SetCommand(Command);
            }
        }

        protected sealed override void OnInitViewComponent(ViewComponent viewComponent)
        {
            base.OnInitViewComponent(viewComponent);
            if (viewComponent is ICommandInvoker commandInvoker)
            {
                commandInvokers.Add(commandInvoker);
            }
        }

        protected abstract void OnCommandSet(TCommand command);
        
    }
}