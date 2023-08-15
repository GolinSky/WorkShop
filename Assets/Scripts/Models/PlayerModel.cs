using System;
using LightWeightFramework.Model;
using UnityEngine;
using WorkShop.Models.Animators;
using WorkShop.Models.TransformModels;

namespace WorkShop.Models
{
    public interface IPlayerModelObserver:IModelObserver, IAnimationModelObserver, ITransformModelObserver
    {
   
    }

    public class PlayerModel: IModel, IPlayerModelObserver
    {
        public event Action<Vector3> OnPositionChanged;

        public Vector3 Position { get; private set; }
        public Vector3 Direction { get; private set; }

        public Vector3 PureDirection { get;  set;}

        
        public void UpdatePosition(Vector3 position, Vector3 direction)
        {
            Position = position;
            Direction = direction;
            OnPositionChanged?.Invoke(Position);
        }
        
        

    }
}