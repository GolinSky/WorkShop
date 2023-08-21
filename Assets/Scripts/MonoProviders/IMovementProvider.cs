using UnityEngine;
using WorkShop.LightWeightFramework.MonoProviders;

namespace WorkShop.MonoProviders
{
    public interface IMovementProvider : IMonoProvider
    {
        bool IsGrounded { get; }
        Vector3 Velocity { get; }
        Transform Transform { get; }
    }

}