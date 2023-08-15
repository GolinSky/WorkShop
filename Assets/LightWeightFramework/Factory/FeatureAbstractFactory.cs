using System;
using LightWeightFramework.Controller;
using LightWeightFramework.Model;
using WorkShop.LightWeightFramework.Repository;
using Object = UnityEngine.Object;

namespace WorkShop.LightWeightFramework.Factory
{
    public sealed class FeatureAbstractFactory:IFeatureFactory
    {
        private readonly IRepository repository;

        public FeatureAbstractFactory(IRepository repository)
        {
            this.repository = repository;
        }
        
        public TController CreateController<TController, TModel>(TModel model)
            where TController : Controller<TModel>
            where TModel : IModel
        {
            return (TController)Activator.CreateInstance(typeof(TController), model);
        }

        public Views.View CreateView(string entityId)
        {
            return  Object.Instantiate(repository.Load<Views.View>(entityId));
        }


    }
}