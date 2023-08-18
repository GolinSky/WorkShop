using UnityEngine;
using WorkShop.LightWeightFramework.Game;
using WorkShop.Models.TransformModels;
using Component = WorkShop.LightWeightFramework.Components.Component;

namespace WorkShop.Components.Controller
{
    public class MoveComponent:Component
    {
        private Vector3 position;
        private readonly ITransformModelObserver model;

        public MoveComponent(ITransformModelObserver model)
        {
            this.model = model;
        }

        protected override void OnInit(IGameObserver gameObserver)
        {
            position = model.Position;
            model.UpdatePosition(position, Vector3.zero);

        }

        protected override void OnRelease()
        {
        }

        public void Move(float deltaTime, Vector3 direction)
        {
          //  model.PureDirection = direction;
            direction *= deltaTime;
            position += direction * model.Speed;//todo:use values from model - like speed and etc...
            model.UpdatePosition(position, direction);
        }

        public void SetPosition(Vector3 position, Vector3 direction)
        {
            this.position = position;
            model.UpdatePosition(position, direction);
        }
    }
}