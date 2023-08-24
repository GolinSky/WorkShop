using System;
using System.Collections.Generic;
using LightWeightFramework.Model;
using UnityEngine;
using WorkShop.Models.Animators;
using WorkShop.Models.TransformModels;

namespace WorkShop.Models
{
    public interface IPlayerModelObserver : IModelObserver
    {

    }

    [Serializable]
    [CreateAssetMenu(fileName = "PlayerModel", menuName = "Models/PlayerModel", order = 1)]
    public class PlayerModel : Model, IPlayerModelObserver
    {
        [field: SerializeField] public TransformModel TransformModel { get; private set; }
        [field: SerializeField] public AnimationModel AnimationModel { get; private set; }


        protected override void OnInit()
        {
            base.OnInit();
            CurrentModels.Add(TransformModel);
            CurrentModels.Add(AnimationModel);
        }
    }
}