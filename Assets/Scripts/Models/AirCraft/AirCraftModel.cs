using LightWeightFramework.Model;
using UnityEngine;

namespace WorkShop.Models.AirCraft
{
    public interface IAirCraftModelObserver:IModelObserver
    {
        
    }
    
    [CreateAssetMenu(fileName = "AirCraftModel", menuName = "Models/AirCraftModel")]
    public class AirCraftModel:Model, IAirCraftModelObserver
    {
        
    }
}