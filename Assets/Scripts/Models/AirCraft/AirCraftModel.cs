using System;
using LightWeightFramework.Model;
using UnityEngine;
using WorkShop.Models.TransformModels;
using WorkShop.Services.Player;

namespace WorkShop.Models.AirCraft
{
    public interface IAirCraftModelObserver:IModelObserver
    {
        event Action<PlayerControlState> OnControlStateChanged;

        float Throttle { get; }

    }
    
    [CreateAssetMenu(fileName = "AirCraftModel", menuName = "Models/AirCraftModel")]
    public class AirCraftModel:Model, IAirCraftModelObserver
    {
        [SerializeField] private TransformModel transformModel;

        public float Throttle { get; set; }

        protected override void OnInit()
        {
            base.OnInit();
            AddInnerModel(transformModel);
        }

        public event Action<PlayerControlState> OnControlStateChanged;

        public PlayerControlState ControlState
        {
            set => OnControlStateChanged?.Invoke(value);
        }

    }
}