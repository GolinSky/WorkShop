using System.Collections.Generic;
using UnityEngine;

namespace LightWeightFramework.Model
{
    public abstract class Model:ScriptableObject, IModel
    {
        protected virtual List<IModel> CurrentModels { get; } = new List<IModel>(); 
        
        public void Init()
        {
            OnInit();
        }
        
        public TModelObserver GetModelObserver<TModelObserver>() where TModelObserver : IModelObserver
        {
            return GetModelInternal<TModelObserver>();
        }

        public TModelObserver GetModel<TModelObserver>() where TModelObserver : IModel
        {
            return GetModelInternal<TModelObserver>();
        }

        private TModelObserver GetModelInternal<TModelObserver>() 
        {
            foreach (var model in CurrentModels)
            {
                if (model is TModelObserver modelObserver)
                {
                    return modelObserver;
                }
            }

            return default;
            
        }

        protected virtual void OnInit(){}
    }
}