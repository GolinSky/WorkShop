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

        void UpdatePosition(Vector3 position, Vector3 direction);
    }
}