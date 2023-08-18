using UnityEngine;
using WorkShop.LightWeightFramework.Game;
using WorkShop.Models.Animators;
using WorkShop.Services.Player;
using Component = WorkShop.LightWeightFramework.Components.Component;

namespace WorkShop.Components.Controller
{
    public class AnimationComponent:Component
    {
        private readonly IAnimationModelObserver model;

        public AnimationComponent(IAnimationModelObserver model)
        {
            this.model = model;
        }
        protected override void OnInit(IGameObserver gameObserver)
        {
        }

        protected override void OnRelease()
        {
        }

        public void Update(Vector3 direction)
        {
            model.PureDirection = direction;
        }
    }
}