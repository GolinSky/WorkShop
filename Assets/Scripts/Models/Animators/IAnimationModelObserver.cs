using LightWeightFramework.Model;
using UnityEngine;
using WorkShop.Models.TransformModels;

namespace WorkShop.Models.Animators
{
    public interface IAnimationModelObserver:ITransformModelObserver
    {
        Vector3 PureDirection { get; }
    }

    public interface IAnimationModel:IAnimationModelObserver
    {
        new Vector3 PureDirection { get; set; }

    }
}