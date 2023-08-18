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
        [SerializeField] private string groundedName = "Grounded";

        private int blendXNameHash;
        private int blendYNameHash;
        private int groundedNameHash;
        
        protected override void OnInit()
        {
            base.OnInit();
            Model.OnPositionChanged += UpdateAnimator;
            blendXNameHash = Animator.StringToHash(blendXName);
            blendYNameHash = Animator.StringToHash(blendYName);
            groundedNameHash = Animator.StringToHash(groundedName);
        }
        
        protected override void OnRelease()
        {
            Model.OnPositionChanged -= UpdateAnimator;
        }

        private void UpdateAnimator(Vector3 position)
        {
            var direction = Model.PureDirection;
            
            animator.SetFloat(blendXNameHash, direction.x);
            animator.SetFloat(blendYNameHash, direction.z);
            animator.SetBool(groundedNameHash, Model.Grounded);
            Debug.Log($"AnimatorViewComponent.Grounded:{Model.Grounded}");
        }
    }
}