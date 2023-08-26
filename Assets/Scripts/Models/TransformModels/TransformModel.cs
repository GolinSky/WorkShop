using System;
using LightWeightFramework.Model;
using UnityEngine;

namespace WorkShop.Models.TransformModels
{
    [CreateAssetMenu(fileName = "TransformModel", menuName = "Models/TransformModel")]
    public class TransformModel : Model, ITransformModel
    {
        public event Action<Vector3> OnPositionChanged;
        public event Action<Vector3> OnDirectionChanged;
        public event Action OnJump;
        public event Action<Transform> OnParentSet;

        [field: SerializeField] public float JumpHeight { get; private set; }
        [field: SerializeField] public float Gravity { get; private set; }
        [field: SerializeField] public float JumpTimeout { get; private set; }
        [field: SerializeField] public float RotationSmoothTime { get; private set; }
        [field: SerializeField] public float SpeedChangeRate { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public float SprintSpeed { get; private set; }

        public Quaternion Rotation { get; set; }
  

        public Vector3 Velocity { get; set; }
        public float Speed { get; set; }
        public float InputMagnitude { get; set; }
        public bool Grounded { get; set; }

        public void InvokeJumpEvent()
        {
            OnJump?.Invoke();
        }

        public void UpdatePosition(Vector3 position, Vector3 direction)
        {
            OnPositionChanged?.Invoke(position);
        }

        public void UpdateDirection(Vector3 direction)
        {
            OnDirectionChanged?.Invoke(direction);
        }
        
        public void SetParent(Transform transform)
        {
            OnParentSet?.Invoke(transform);
        }
    }
}