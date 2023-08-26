using System;
using LightWeightFramework.Model;
using UnityEngine;
using WorkShop.Models.TransformModels;

namespace WorkShop.Models.Animators
{
    [Serializable]
    [CreateAssetMenu(fileName = "AnimationModel", menuName = "Models/AnimationModel")]
    public class AnimationModel : Model, IAnimationModel
    {
        public event Action OnUpdateData;

        [field: SerializeField] public float FallTimeout { get; private set; }
        
        public ITransformModel CurrentTransformModel { get; set; }

        public ITransformModelObserver CurrentTransformModelObserver => CurrentTransformModel;

        public float AnimationBlend { get; set; }
        public bool IsJumped { get; set; }
        public bool IsFall { get; set; }

        public void InvokeUpdateEvent()
        {
            OnUpdateData?.Invoke();
        }
    }
}