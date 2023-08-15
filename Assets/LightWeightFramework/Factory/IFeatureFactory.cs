using LightWeightFramework.Controller;
using LightWeightFramework.Model;

namespace WorkShop.LightWeightFramework.Factory
{
    public interface IFeatureFactory
    {
        TController CreateController<TController, TModel>(TModel model)
            where TController : Controller<TModel>
            where TModel : IModel;

  
        Views.View CreateView(string entityId);
    }
}