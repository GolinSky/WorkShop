using System;
using LightWeightFramework.Model;
using UnityEngine;

namespace WorkShop.Models
{
    public interface IPlayerModelObserver:IModelObserver
    {
        event Action<Vector3> OnPositionChanged;
        Vector3 Position { get; }
    }

    public class PlayerModel: IModel, IPlayerModelObserver
    {
        public event Action<Vector3> OnPositionChanged;

        public Vector3 Position { get; private set; }
        
        public void UpdatePosition(Vector3 position)
        {
            Position = position;
            OnPositionChanged?.Invoke(Position);
        }
    }
}