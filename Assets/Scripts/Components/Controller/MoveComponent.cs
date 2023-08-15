using UnityEngine;
using WorkShop.LightWeightFramework.Game;
using WorkShop.Models.TransformModels;
using WorkShop.Services.Player;
using Component = WorkShop.LightWeightFramework.Components.Component;

namespace WorkShop.Components.Controller
{
    public class MoveComponent:Component
    {
        private IInputService inputService;
        private Vector3 direction;
        private Vector3 position;
        private readonly ITransformModelObserver model;


        public MoveComponent(ITransformModelObserver model)
        {
            this.model = model;
        }


        protected override void OnInit(IGameObserver gameObserver)
        {
            position = new Vector3(0, 10, 0);
            model.UpdatePosition(position, Vector3.zero);
            inputService = GameObserver.ServiceHub.Get<IInputService>();

        }

        protected override void OnRelease()
        {
        }

        public void Move(float deltaTime)
        {
            direction.x = inputService.UserInput.x;
            direction.z = inputService.UserInput.y;
            direction.y = Physics.gravity.y;
          //  model.PureDirection = direction;
            direction *= deltaTime;
            position += direction;//todo:use values from model - like speed and etc...
            model.UpdatePosition(position, direction);
        }
    }
}