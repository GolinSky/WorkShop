using System;
using UnityEngine;

namespace WorkShop.Models.TransformModels
{
    public interface ITransformModelObserver : IBaseTransformModelObserver
    {
        event Action OnJump; 

        float JumpHeight { get; }
        float Gravity { get; }
        float JumpTimeout { get; }
        float RotationSmoothTime { get; }
        float SpeedChangeRate { get; }
        float MoveSpeed { get; }
        float SprintSpeed { get; }
        float InputMagnitude { get; }
        bool Grounded { get; }
        Quaternion Rotation { get; }
    }

    public interface ITransformModel :IBaseTransformModel, ITransformModelObserver
    {
        void InvokeJumpEvent();
        new bool Grounded { get; set; }
        new float InputMagnitude { get; set; }
        Vector3 Velocity { get; set; }
        new Quaternion Rotation { get; set; }
    }
}