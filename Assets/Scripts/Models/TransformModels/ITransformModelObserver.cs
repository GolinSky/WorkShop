using System;
using LightWeightFramework.Model;
using UnityEngine;

namespace WorkShop.Models.TransformModels
{
    public interface ITransformModelObserver : IModelObserver
    {
        event Action OnJump; 
        event Action<Vector3> OnPositionChanged;
        float JumpHeight { get; }
        float Gravity { get; }
        float JumpTimeout { get; }
        float RotationSmoothTime { get; }
        float SpeedChangeRate { get; }
        bool Grounded { get; }
        float MoveSpeed { get; }
        float SprintSpeed { get; }
        float Speed { get; }
        float InputMagnitude { get; }
        Vector2 MoveDirection { get; }
        float VerticalVelocity { get; }
        Vector3 Velocity { get; }
    }

    public interface ITransformModel : ITransformModelObserver
    {
        void UpdatePosition(Vector3 position, Vector3 direction);
        new bool Grounded { get; set; }
        new float Speed { get; set; }
        new float InputMagnitude { get; set; }
        new Vector2 MoveDirection { get; set; }
        new float VerticalVelocity { get; set; }
        new Vector3 Velocity { get; set; }

        void InvokeJumpEvent();

    }
}