using UnityEngine;
using WorkShop.LightWeightFramework.Game;
using WorkShop.Models;
using WorkShop.Models.Animators;
using WorkShop.Models.Input;
using WorkShop.Models.TransformModels;
using Component = WorkShop.LightWeightFramework.Components.Component;

namespace WorkShop.Components.Controller
{
    //todo:need to create player anim component
    public class AnimationComponent:Component
    {
        private const float ZeroSpeed = 0f;

        private readonly IAnimationModel model;
        private readonly IInputModelObserver inputModel;
        private ITransformModelObserver transformModelObserver;
        private float animationBlend;
        private float fallTimeoutDelta;

        public AnimationComponent(PlayerModel playerModel, IInputModelObserver inputModel)
        {
            model = playerModel.GetModel<IAnimationModel>();
            model.CurrentTransformModel = playerModel.GetModel<ITransformModel>();
            this.inputModel = inputModel;
            fallTimeoutDelta = model.FallTimeout;
            transformModelObserver = model.CurrentTransformModelObserver;

        }
        protected override void OnInit(IGameObserver gameObserver)
        {
            transformModelObserver.OnJump += OnJump;
        }

        protected override void OnRelease()
        {
            transformModelObserver.OnJump -= OnJump;
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
                    ? transformModelObserver.SprintSpeed
                    : transformModelObserver.MoveSpeed;
            
            animationBlend = Mathf.Lerp(animationBlend, targetSpeed, deltaTime * transformModelObserver.SpeedChangeRate);
            
            if (animationBlend < 0.01f)// magic number with float prec - fix this
            {
                animationBlend = 0f;
            }

            model.AnimationBlend = animationBlend;
            if (transformModelObserver.Grounded)
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