using System;
using LightWeightFramework.Model;
using UnityEngine;

namespace WorkShop.Models.TransformModels
{
    public interface IBaseTransformModelObserver:IModelObserver
    {
        event Action<Vector3> OnPositionChanged;

    }
    
    public interface IBaseTransformModel:IModel, IBaseTransformModelObserver
    {
        void UpdatePosition(Vector3 position, Vector3 direction);

    }
}