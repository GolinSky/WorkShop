using System.Collections.Generic;
using LightWeightFramework.Controller;
using UnityEngine;
using WorkShop.Commands.Player;
using WorkShop.Components.Controller;
using WorkShop.LightWeightFramework.Command;
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
        private IInputService inputService;
        private Vector3 direction;

        public override string Id => "Player";

        public PlayerController(PlayerModel model) : base(model) {}

       
        protected override void OnInit()
        {
            base.OnInit();
            inputService = GameObserver.ServiceHub.Get<IInputService>();

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
            direction.x = inputService.UserInput.x;
            direction.z = inputService.UserInput.y;
            direction.y = Physics.gravity.y;
            
            moveComponent.Move(deltaTime, direction);
            animationComponent.Update(direction);

        }

        public override ICommand GetCommand()
        {
            return new PlayerCommand(this, GameObserver);
        }

   
    }
}