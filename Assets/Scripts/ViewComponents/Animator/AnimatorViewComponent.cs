using UnityEngine;
using WorkShop.LightWeightFramework.ViewComponents;
using WorkShop.Models.Animators;

namespace WorkShop.ViewComponents
{
    public class AnimatorViewComponent:ViewComponent<IAnimationModelObserver>
    {
        [SerializeField] private Animator animator;
        [SerializeField] private string blendXName = "Horizontal";
        [SerializeField] private string blendYName = "Vertical";
        
        protected override void OnInit()
        {
            base.OnInit();
            Model.OnPositionChanged += UpdateAnimator;
        }
        
        protected override void OnRelease()
        {
            Model.OnPositionChanged -= UpdateAnimator;
        }

        private void UpdateAnimator(Vector3 position)
        {
            var direction = Model.PureDirection;
            
            animator.SetFloat(blendXName, direction.x);
            animator.SetFloat(blendYName, direction.z);
        }
    }
}