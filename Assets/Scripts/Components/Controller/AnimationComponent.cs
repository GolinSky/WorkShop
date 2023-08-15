using UnityEngine;
using WorkShop.LightWeightFramework.Game;
using WorkShop.Models.Animators;
using WorkShop.Services.Player;
using Component = WorkShop.LightWeightFramework.Components.Component;

namespace WorkShop.Components.Controller
{
    public class AnimationComponent:Component
    {
        private IInputService inputService;
        private Vector3 direction;
        private readonly IAnimationModelObserver model;

        public AnimationComponent(IAnimationModelObserver model)
        {
            this.model = model;
        }
        protected override void OnInit(IGameObserver gameObserver)
        {
            inputService = GameObserver.ServiceHub.Get<IInputService>();
 
        }

        protected override void OnRelease()
        {
        }

        public void Update()
        {
            direction.x = inputService.UserInput.x;
            direction.z = inputService.UserInput.y;
            direction.y = Physics.gravity.y;
            model.PureDirection = direction;
        }
    }
}