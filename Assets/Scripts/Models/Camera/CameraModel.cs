using System;
using LightWeightFramework.Model;
using UnityEngine;
using WorkShop.Models.TransformModels;

namespace WorkShop.Models.Camera
{
    public interface ICameraModelObserver:ITransformModelObserver
    {
        Vector3 Distance { get; }
        Vector3 LookAtPosition { get; }
    }

    public interface ICameraModel:ICameraModelObserver, ITransformModel
    {
        
    }

    [Serializable]
    [CreateAssetMenu(fileName = "CameraModel", menuName = "Models/CameraModel", order = 1)]
    public class CameraModel : Model, ICameraModel
    {
        public event Action<Vector3> OnPositionChanged;
        public Vector3 Position { get; private set; }
        public Vector3 Direction { get; private set; }
        public Vector3 LookAtPosition { get; set; }

        [field: SerializeField] public float YMin { get; private set; }
        [field: SerializeField] public float YMax { get; private set; }
        [field: SerializeField] public float Sensitivity { get; private set; }

        [field: SerializeField] public Vector3 Distance { get; private set; }

        [field: SerializeField] public float Speed { get; private set; }
        
        public bool Grounded { get; set; }


        public void UpdatePosition(Vector3 position, Vector3 direction)
        {
            Position = position;
            Direction = direction;
            OnPositionChanged?.Invoke(position);
        }
    }
}