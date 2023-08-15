using LightWeightFramework.Model;
using WorkShop.LightWeightFramework.Game;

namespace LightWeightFramework.Controller
{
    public abstract class Controller<TModel>:IController
        where TModel : IModel
    {
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
            OnInit();
        }

        public void Release()
        {
            OnRelease();
        }
        
        protected virtual void OnInit(){}
        protected virtual void OnRelease(){}
    }
}