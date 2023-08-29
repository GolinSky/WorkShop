using System;
using LightWeightFramework.Model;
using UnityEngine;
using WorkShop.Models.Animators;
using WorkShop.Models.TransformModels;
using WorkShop.Services.Player;

namespace WorkShop.Models
{
    public interface IPlayerModelObserver : IModelObserver
    {
        event Action<PlayerControlState> OnControlStateChanged;

    }

    [Serializable]
    [CreateAssetMenu(fileName = "PlayerModel", menuName = "Models/PlayerModel", order = 1)]
    public class PlayerModel : Model, IPlayerModelObserver
    {
        [SerializeField] private TransformModel transformModel;
        [SerializeField] private AnimationModel animationModel;
        

        protected override void OnInit()
        {
            base.OnInit();
            AddInnerModel(transformModel);
            AddInnerModel(animationModel);
        }

        public event Action<PlayerControlState> OnControlStateChanged;

        public PlayerControlState ControlState
        {
            set => OnControlStateChanged?.Invoke(value);
        }
    }
}