using UnityEngine;
using WorkShop.LightWeightFramework.ViewComponents;
using WorkShop.Models.Animators;

namespace WorkShop.ViewComponents
{
    public class AnimatorViewComponent:ViewComponent<IAnimationModelObserver>
    {
        [SerializeField] private Animator animator;
        
        private int animIDSpeed;
        private int animIDGrounded;
        private int animIDJump;
        private int animIDFreeFall;
        private int animIDMotionSpeed;


        protected override void OnInit()
        {
            base.OnInit();
            animIDSpeed = Animator.StringToHash("Speed");
            animIDGrounded = Animator.StringToHash("Grounded");
            animIDJump = Animator.StringToHash("Jump");
            animIDFreeFall = Animator.StringToHash("FreeFall");
            animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
            
            
            Model.OnUpdateData += UpdateAnimator;
        }
        
        protected override void OnRelease()
        {
            Model.OnUpdateData -= UpdateAnimator;
        }

        private void UpdateAnimator()
        {
            animator.SetBool(animIDGrounded, Model.CurrentTransformModelObserver.Grounded);
            animator.SetFloat(animIDSpeed, Model.AnimationBlend);
            animator.SetFloat(animIDMotionSpeed, Model.CurrentTransformModelObserver.InputMagnitude);
            animator.SetBool(animIDJump, Model.IsJumped);
            animator.SetBool(animIDFreeFall, Model.IsFall);
        }
    }
}