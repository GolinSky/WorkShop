using System;
using LightWeightFramework.Model;
using WorkShop.Models.TransformModels;

namespace WorkShop.Models.Animators
{
    public interface IAnimationModelObserver : IModelObserver
    {
        float FallTimeout { get; }
        float AnimationBlend { get; }
        bool IsJumped { get; }
        bool IsFall { get; }
        event Action OnUpdateData;
        
        ITransformModelObserver CurrentTransformModelObserver { get; }

    }

    public interface IAnimationModel :IModel, IAnimationModelObserver
    {
        new float AnimationBlend { get; set; }
        new bool IsJumped { get; set; }
        new bool IsFall { get; set; }
        ITransformModel CurrentTransformModel { get; set; }
        void InvokeUpdateEvent();
        
        
    }
}