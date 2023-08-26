using UnityEngine;

namespace WorkShop.Strategy
{
    public interface IMovementStrategy
    {
        void Move(float deltaTime, Vector3 direction);
    }
}