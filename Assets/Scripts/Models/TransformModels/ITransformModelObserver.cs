using System;
using LightWeightFramework.Model;
using UnityEngine;

namespace WorkShop.Models.TransformModels
{
    public interface ITransformModelObserver:IModelObserver
    {
        event Action<Vector3> OnPositionChanged;

        Vector3 Position { get; }
        Vector3 Direction { get; }
        float Speed { get; }
        
        bool Grounded { get; }

    }

    public interface ITransformModel:ITransformModelObserver
    {
        void UpdatePosition(Vector3 position, Vector3 direction);
        new bool Grounded { get; set; }


    }
}