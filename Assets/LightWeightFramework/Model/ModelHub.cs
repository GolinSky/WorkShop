using System.Collections.Generic;

namespace LightWeightFramework.Model
{
    public interface IModelHub
    {
        TModel GetModel<TModel>() where TModel:IModel, new();
    }
    
    public class ModelHub:IModelHub
    {
        private List<IModel> modelsList = new List<IModel>();
        public TModel GetModel<TModel>()
            where TModel:IModel, new()
        {
            foreach (var model in modelsList)
            {
                if (model is TModel targetModel)
                {
                    return targetModel;
                }
            }

            var newModel = new TModel();
            modelsList.Add(newModel);
            return newModel;
        }
    }
}