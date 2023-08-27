using UnityEngine;

namespace WorkShop.Strategy
{
    public interface IMovementStrategy:IStrategy
    {
        void Move(float deltaTime, Vector3 direction);
    }
}