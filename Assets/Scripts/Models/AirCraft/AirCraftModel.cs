using LightWeightFramework.Model;
using UnityEngine;
using UnityEngine.Serialization;
using WorkShop.Models.TransformModels;

namespace WorkShop.Models.AirCraft
{
    public interface IAirCraftModelObserver:IModelObserver
    {
        
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
    }
}