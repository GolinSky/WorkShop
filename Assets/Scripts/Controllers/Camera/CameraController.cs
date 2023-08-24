using System.Collections.Generic;
using LightWeightFramework.Controller;
using UnityEngine;
using WorkShop.Components.Controller;
using WorkShop.LightWeightFramework.Components;
using WorkShop.LightWeightFramework.UpdateService;
using WorkShop.Models.Camera;
using WorkShop.Models.Input;
using WorkShop.Services.Player;

namespace WorkShop.Controllers.Camera
{
    public class CameraController : Controller<CameraModel>, ITick
    {
        private IPlayerService playerService;
        
        private MoveComponent moveComponent;
        private Vector3 prevMousePos;
        private Vector3 cameraPosition;
        
        private float currentX = 0.0f;
        private float currentY = 0.0f;
        private float prevDistance;
        private IInputModelObserver inputModel;


        public override string Id => "Camera";

        public CameraController(CameraModel model) : base(model)
        {
        }

        protected override List<IComponent> BuildsComponents()
        {
            var components = base.BuildsComponents();
            components.Add(new UpdateComponent(this));
            components.Add(new MoveComponent(Model));

            return components;
        }

        protected override void OnInit()
        {
            base.OnInit();
            playerService = GetService<IPlayerService>();
            moveComponent = GetComponent<MoveComponent>();
            inputModel = GameObserver.ModelHub.GetModel<IInputModelObserver>();
        }

        public void Notify(float deltaTime)
        {
            cameraPosition = playerService.PlayerPosition; //+ ;
            currentX += inputModel.Look.x * Model.Sensitivity * deltaTime;
            currentY += inputModel.Look.y * Model.Sensitivity * deltaTime;
            currentY = Mathf.Clamp(currentY, Model.YMin, Model.YMax);
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            cameraPosition += rotation * Model.Distance;
            Model.LookAtPosition = playerService.PlayerPosition;
            moveComponent.SetPosition(cameraPosition, Vector3.zero);
        }
    }
}