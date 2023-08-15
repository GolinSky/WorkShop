using LightWeightFramework.Model;
using UnityEngine;
using WorkShop.LightWeightFramework.Command;

namespace WorkShop.LightWeightFramework.Views
{
    public abstract class View : MonoBehaviour, IView
    {
        public abstract void Init(IModelObserver model);
        public abstract void Release();
    }

    public abstract class View<TModel> : View
        where TModel:IModelObserver
    {
        protected TModel Model { get; private set; }
        
        public sealed override void Init(IModelObserver model)
        {
            Model = (TModel)model;
            OnInit(Model);
        }

        public sealed override void Release()
        {
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