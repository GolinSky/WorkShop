using System;
using UnityEngine;

namespace WorkShop.Models.TransformModels
{
    public interface ITransformModelObserver : IBaseTransformModelObserver
    {
        event Action OnJump; 
        Vector2 MoveDirection { get; }

        float JumpHeight { get; }
        float Gravity { get; }
        float JumpTimeout { get; }
        float RotationSmoothTime { get; }
        float SpeedChangeRate { get; }
        float MoveSpeed { get; }
        float SprintSpeed { get; }
        float Speed { get; }
        float InputMagnitude { get; }
        float VerticalVelocity { get; }
        bool Grounded { get; }
    }

    public interface ITransformModel :IBaseTransformModel, ITransformModelObserver
    {
        void InvokeJumpEvent();
        new Vector2 MoveDirection { get; set; }
        new bool Grounded { get; set; }
        new float Speed { get; set; }
        new float InputMagnitude { get; set; }
        new float VerticalVelocity { get; set; }
        Vector3 Velocity { get; set; }

    }
}