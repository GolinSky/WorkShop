using System;
using LightWeightFramework.Model;
using UnityEngine;
using WorkShop.Models.Animators;
using WorkShop.Models.TransformModels;

namespace WorkShop.Models
{
    public interface IPlayerModelObserver : IModelObserver, IAnimationModel, ITransformModel
    {
    }

    [Serializable]
    [CreateAssetMenu(fileName = "PlayerModel", menuName = "Models/PlayerModel", order = 1)]
    public class PlayerModel : Model, IPlayerModelObserver
    {
        public event Action OnUpdateData;
        public event Action OnJump;
        public event Action<Vector3> OnPositionChanged;

        [field: SerializeField] public float JumpHeight { get; private set; }
        [field: SerializeField] public float Gravity { get; private set; }
        [field: SerializeField] public float JumpTimeout { get; private set; }
        [field: SerializeField] public float RotationSmoothTime { get; private set; }
        [field: SerializeField] public float SpeedChangeRate { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public float SprintSpeed { get; private set; }
        [field: SerializeField] public float FallTimeout { get; private set; }
        
        public float AnimationBlend { get; set; }
        public bool IsJumped { get; set; }
        public bool IsFall { get; set; }
        public float Speed { get; set; }
        public float InputMagnitude { get; set; }
        public Vector2 MoveDirection { get; set; }
        public float VerticalVelocity { get; set; }
        public bool Grounded { get; set; }
        public bool CanJump { get; set; }
        public Vector3 Velocity { get; set; }
        

        public void InvokeJumpEvent()
        {
            OnJump?.Invoke();
        }

        public void InvokeUpdateEvent()
        {
            OnUpdateData?.Invoke();
        }

        public void UpdatePosition(Vector3 position, Vector3 direction)
        {
            OnPositionChanged?.Invoke(position);
        }

        public override string ToString()
        {
            return $"Velocity:{Velocity}, Speed:{Speed}, Grounded:{Grounded}";
        }
    }
}