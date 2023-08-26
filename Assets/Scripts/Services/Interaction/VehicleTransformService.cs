using UnityEngine;
using WorkShop.LightWeightFramework.Game;
using WorkShop.LightWeightFramework.Service;

namespace WorkShop.Services.Interaction
{
    public interface IVehicleTransformService:IService
    {
        Transform CurrentVehicleTransform { get; }
        void UpdateCurrentVehicleTransform(Transform transform);
    }
    
    public class VehicleTransformService:Service, IVehicleTransformService
    {
        public Transform CurrentVehicleTransform { get; private set; }

        protected override void OnInit(IGameObserver gameObserver)
        {
        }

        protected override void Release()
        {
        }

        public void UpdateCurrentVehicleTransform(Transform transform)
        {
            CurrentVehicleTransform = transform;
        }
    }
}