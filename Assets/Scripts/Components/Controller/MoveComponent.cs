using UnityEngine;
using WorkShop.LightWeightFramework.Components;
using WorkShop.LightWeightFramework.Game;
using WorkShop.Models.TransformModels;
using Component = WorkShop.LightWeightFramework.Components.Component;

namespace WorkShop.Components.Controller
{
    public interface IMoveComponent:IComponent
    {
        void Move(float deltaTime, Vector3 direction);
    }
    public class MoveComponent:Component, IMoveComponent
    {
        private Vector3 position;
        private readonly ITransformModel model;

        public MoveComponent(ITransformModel model)
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
            model.UpdatePosition(position, direction);
        }

        public void SetPosition(Vector3 position, Vector3 direction)
        {
            this.position = position;
            model.UpdatePosition(position, direction);
        }
    }
}