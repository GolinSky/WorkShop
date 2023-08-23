using System;
using WorkShop.Models.TransformModels;

namespace WorkShop.Models.Animators
{
    public interface IAnimationModelObserverObserver : ITransformModelObserverObserver
    {
        float FallTimeout { get; }
        float AnimationBlend { get; }
        bool IsJumped { get; }
        bool IsFall { get; }
        event Action OnUpdateData;
    }

    public interface IAnimationModel : IAnimationModelObserverObserver
    {
        new float AnimationBlend { get; set; }
        new bool IsJumped { get; set; }
        new bool IsFall { get; set; }
        void InvokeUpdateEvent();
    }
}