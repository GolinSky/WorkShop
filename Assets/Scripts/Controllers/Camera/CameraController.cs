using System.Collections.Generic;
using LightWeightFramework.Controller;
using UnityEngine;
using WorkShop.Components.Controller;
using WorkShop.LightWeightFramework.Components;
using WorkShop.LightWeightFramework.UpdateService;
using WorkShop.Models.Camera;
using WorkShop.Services.Player;

namespace WorkShop.Controllers.Camera
{
    public class CameraController : Controller<CameraModel>, ITick
    {
        private IPlayerService playerService;
        private MoveComponent moveComponent;
        private Vector3 _prevMousePos;
        private Vector3 cameraPosition;
        
        
        private const float YMin = -50.0f;
        private const float YMax = 50.0f;



        private float currentX = 0.0f;
        private float currentY = 0.0f;
        public float sensivity = 4.0f;

        float prevDistance;

        
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

        }

        protected override void OnRelease()
        {
            base.OnRelease();
        }
        
        public void Notify(float state)
        {
            cameraPosition = playerService.PlayerPosition;//+ ;
            
            
            currentX += Input.GetAxis("Mouse X") * sensivity * state;
            currentY += Input.GetAxis("Mouse Y") * sensivity * state;

            currentY = Mathf.Clamp(currentY, YMin, YMax);

            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            cameraPosition += rotation * Model.Distance;

            Model.LookAtPosition = playerService.PlayerPosition;
            
            moveComponent.SetPosition(cameraPosition, Vector3.zero);
        }
    }
}