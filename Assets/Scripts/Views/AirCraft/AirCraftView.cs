using UnityEngine;
using WorkShop.Commands.AirCraft;
using WorkShop.LightWeightFramework;
using WorkShop.Models.AirCraft;
using WorkShop.Models.TransformModels;
using WorkShop.ViewComponents;

namespace WorkShop.Views
{
    public class AirCraftView:View<IAirCraftModelObserver, IAirCraftCommand>, IInteractable
    {
        [SerializeField] private Rigidbody rigidbodyBehaviour;
        [SerializeField] private Transform playerSitTransform;
        
        private ITransformModelObserver transformModelObserver;
        protected override void OnInit(IAirCraftModelObserver model)
        {
            transformModelObserver = model.GetModelObserver<ITransformModelObserver>();
            transformModelObserver.OnDirectionChanged += OnDirectionChanged;
        }

        protected override void OnRelease()
        {
            transformModelObserver.OnDirectionChanged -= OnDirectionChanged;
        }
        
        private void OnDirectionChanged(Vector3 direction)
        {
            rigidbodyBehaviour.AddForce(direction, ForceMode.Acceleration);
        }

        protected override void OnCommandSet(IAirCraftCommand command)
        {
        }

        public bool TryInteract()
        {
            return Command.TryInteract(playerSitTransform);
        }
    }
}