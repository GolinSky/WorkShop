using System.Collections.Generic;
using LightWeightFramework.Controller;
using UnityEngine;
using WorkShop.Components.Controller;
using WorkShop.LightWeightFramework.Components;
using WorkShop.LightWeightFramework.UpdateService;
using WorkShop.Models;
using WorkShop.Services.Player;

namespace WorkShop.Controllers
{
    public class PlayerController: Controller<PlayerModel>, ITick
    {
        private MoveComponent moveComponent;
        private AnimationComponent animationComponent;
        public override string Id => "Player";

        public PlayerController(PlayerModel model) : base(model)
        {
           
        }

        protected override void OnInit()
        {
            base.OnInit();
            moveComponent = GetComponent<MoveComponent>();
            animationComponent = GetComponent<AnimationComponent>();
        }

        protected override List<IComponent> BuildsComponents()
        {
            var components = base.BuildsComponents();
            components.Add(new UpdateComponent(this));
            components.Add(new MoveComponent(Model));
            components.Add(new AnimationComponent(Model));
            return components;
        }
        
        public void Notify(float deltaTime)
        {
            moveComponent.Move(deltaTime);
            animationComponent.Update();
        }
    }
}