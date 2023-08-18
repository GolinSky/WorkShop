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
    
    [Serializable]
    [CreateAssetMenu(fileName = "PlayerModel", menuName = "Models/PlayerModel", order = 1)]
    public class PlayerModel: Model, IPlayerModelObserver
    {
        public event Action<Vector3> OnPositionChanged;
        
        [field:SerializeField] public float Speed { get; private set; }
        [field:SerializeField] public Vector3 Position { get; private set; }
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