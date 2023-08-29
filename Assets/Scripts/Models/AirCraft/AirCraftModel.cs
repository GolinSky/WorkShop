using LightWeightFramework.Model;
using UnityEngine;
using WorkShop.Models.TransformModels;

namespace WorkShop.Models.AirCraft
{
    public interface IAirCraftModelObserver:IModelObserver
    {
        float Throttle { get; }
        bool AirBrakes { get; }
    }
    
    [CreateAssetMenu(fileName = "AirCraftModel", menuName = "Models/AirCraftModel")]
    public class AirCraftModel:Model, IAirCraftModelObserver
    {
        [SerializeField] private TransformModel transformModel;


        protected override void OnInit()
        {
            base.OnInit();
            AddInnerModel(transformModel);
        }

        public float Throttle { get; set; }
        public bool AirBrakes { get; set; }
    }
}