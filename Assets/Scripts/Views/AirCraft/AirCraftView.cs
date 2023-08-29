using UnityEngine;
using WorkShop.Commands.AirCraft;
using WorkShop.LightWeightFramework;
using WorkShop.Models.AirCraft;
using WorkShop.Models.TransformModels;
using WorkShop.ViewComponents;

namespace WorkShop.Views
{
    public class AirCraftView:View<IAirCraftModelObserver, IAirCraftCommand>, IInteractableProvider
    {
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

        }

        protected override void OnCommandSet(IAirCraftCommand command)
        {
            command.SetPlayerSit(playerSitTransform);
        }
        
        public IInteractable GetInteractable()
        {
            return Command.Interactable;//get this from model
        }
    }
}