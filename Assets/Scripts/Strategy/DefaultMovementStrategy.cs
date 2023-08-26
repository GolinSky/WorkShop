using UnityEngine;

namespace WorkShop.Strategy
{
    public class DefaultMovementStrategy:IMovementStrategy
    {
        public void Move(float deltaTime, Vector3 direction) {}
    }
}