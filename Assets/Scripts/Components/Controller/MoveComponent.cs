using UnityEngine;
using WorkShop.LightWeightFramework.Components;
using WorkShop.LightWeightFramework.Game;
using WorkShop.Models.TransformModels;
using WorkShop.Strategy;
using Component = WorkShop.LightWeightFramework.Components.Component;

namespace WorkShop.Components.Controller
{
    public interface IMoveComponent:IComponent
    {
        void Move(float deltaTime, Vector3 direction);
    }
    public class MoveComponent:Component, IMoveComponent, IMovementStrategy
    {
        private Vector3 position;
        private readonly IBaseTransformModel model;

        public MoveComponent(IBaseTransformModel model)
        {
            this.model = model;
        }

        protected override void OnInit(IGameObserver gameObserver)
        {
            model.UpdatePosition(position, Vector3.zero);
        }

        protected override void OnRelease()
        {
        }

        public void Move(float deltaTime, Vector3 direction)
        {
            model.UpdateDirection(direction);
        }

        public void SetPosition(Vector3 position, Vector3 direction)
        {
            this.position = position;
            model.UpdatePosition(position, direction);
        }
    }
}