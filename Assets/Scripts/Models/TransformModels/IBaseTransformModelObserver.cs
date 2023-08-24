using System;
using LightWeightFramework.Model;
using UnityEngine;

namespace WorkShop.Models.TransformModels
{
    public interface IBaseTransformModelObserver:IModelObserver
    {
        event Action<Vector3> OnPositionChanged;
        event Action<Vector3> OnDirectionChanged;

    }
    
    public interface IBaseTransformModel:IModel, IBaseTransformModelObserver
    {
        void UpdatePosition(Vector3 position, Vector3 direction);
        void UpdateDirection(Vector3 direction);

    }
}