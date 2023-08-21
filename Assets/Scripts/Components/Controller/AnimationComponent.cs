using UnityEngine;
using WorkShop.LightWeightFramework.Game;
using WorkShop.Models.Animators;
using WorkShop.Models.Input;
using Component = WorkShop.LightWeightFramework.Components.Component;

namespace WorkShop.Components.Controller
{
    //todo:need to create player anim component
    public class AnimationComponent:Component
    {
        private const float ZeroSpeed = 0f;

        private readonly IAnimationModel model;
        private readonly IInputModelObserver inputModel;
        private float animationBlend;
        private float fallTimeoutDelta;

        public AnimationComponent(IAnimationModel model, IInputModelObserver inputModel)
        {
            this.model = model;
            this.inputModel = inputModel;
            fallTimeoutDelta = model.FallTimeout;

        }
        protected override void OnInit(IGameObserver gameObserver)
        {
            model.OnJump += OnJump;
        }

        protected override void OnRelease()
        {
            model.OnJump -= OnJump;
        }
        
        private void OnJump()
        {
            model.IsJumped = true;
        }

        public void Update(float deltaTime)
        {
            float targetSpeed = inputModel.Move == Vector2.zero
                ? ZeroSpeed
                : inputModel.Sprint
                    ? model.SprintSpeed
                    : model.MoveSpeed;
            
            animationBlend = Mathf.Lerp(animationBlend, targetSpeed, deltaTime * model.SpeedChangeRate);
            
            if (animationBlend < 0.01f)// magic number with float prec - fix this
            {
                animationBlend = 0f;
            }

            model.AnimationBlend = animationBlend;
            if (model.Grounded)
            {
                fallTimeoutDelta = model.FallTimeout;
                model.IsFall = false;
            }
            else
            {
                model.IsJumped = false;
                if (fallTimeoutDelta >= 0.0f)
                {
                    fallTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                    model.IsFall = true;
                }
            }
            model.InvokeUpdateEvent();
        }
    }
}