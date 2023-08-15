using System.Collections.Generic;
using LightWeightFramework.Model;
using WorkShop.LightWeightFramework.Components;
using WorkShop.LightWeightFramework.Game;

namespace LightWeightFramework.Controller
{
    public abstract class Controller<TModel>:IController
        where TModel : IModel
    {
        private IEnumerable<IComponent> components;

        public virtual string Id => GetType().Name;
        protected TModel Model { get; private set; }
        IModelObserver IController.Model => Model;
        protected IGameObserver GameObserver { get; private set; }

        protected Controller(TModel model)
        {
            Model = model;
        }

        public void Init(IGameObserver gameObserver)
        {
            GameObserver = gameObserver;
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
        
        protected virtual void OnInit(){}
        protected virtual void OnRelease(){}
    }
}