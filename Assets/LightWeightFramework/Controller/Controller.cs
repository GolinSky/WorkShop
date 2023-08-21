using System.Collections.Generic;
using LightWeightFramework.Model;
using WorkShop.LightWeightFramework.Command;
using WorkShop.LightWeightFramework.Components;
using WorkShop.LightWeightFramework.Game;
using WorkShop.LightWeightFramework.Service;

namespace LightWeightFramework.Controller
{
    public abstract class Controller<TModel>:IController
        where TModel : IModel
    {
        private IEnumerable<IComponent> components;

        public virtual string Id => GetType().Name;
        protected TModel Model { get; private set; }
        public virtual ICommand GetCommand()
        {
            throw new System.NotImplementedException();
        }

        IModelObserver IController.Model => Model;
        protected IGameObserver GameObserver { get; private set; }

        protected Controller(TModel model)
        {
            Model = model;
        }

        public void Init(IGameObserver gameObserver)
        {
            GameObserver = gameObserver;
            OnBeforeComponentsInitialed();
            components = BuildsComponents();
            foreach (var component in components)
            {
                component.Init(gameObserver);
            }
            OnInit();
        }

        public void Release()
        {
            OnRelease();
        }
        
        protected virtual List<IComponent> BuildsComponents()
        {
            return new List<IComponent>();
        }
        
        public TComponent GetComponent<TComponent>() where TComponent : IComponent
        {
            return GetComponentInternal<TComponent>();
        }

        private T GetComponentInternal<T>() // make extension 
        {
            foreach (var component in components)
            {
                if (component is T targetComponent)
                {
                    return targetComponent;
                }
            }

            return default;
        }

        protected TService GetService<TService>() where TService:IService
        {
            return GameObserver.ServiceHub.Get<TService>();
        }
        
        protected virtual void OnInit(){}
        protected virtual void OnBeforeComponentsInitialed(){}
        protected virtual void OnRelease(){}
    }
}