using UnityEngine;
using WorkShop.Commands.AirCraft;
using WorkShop.LightWeightFramework;
using WorkShop.Models.AirCraft;
using WorkShop.Models.TransformModels;
using WorkShop.Services.Player;
using WorkShop.ViewComponents;

namespace WorkShop.Views
{
    public class AirCraftView:View<IAirCraftModelObserver, IAirCraftCommand>, IInteractableProvider
    {
        [SerializeField] private Transform playerSitTransform;
        [SerializeField] private AirCraftMonoBehaviour airCraftMonoBehaviour;
        
        private ITransformModelObserver transformModelObserver;
        
        protected override void OnInit(IAirCraftModelObserver model)
        {
            transformModelObserver = model.GetModelObserver<ITransformModelObserver>();
            transformModelObserver.OnDirectionChanged += OnDirectionChanged;
            Model.OnControlStateChanged += OnControlStateChanged;
        }

        protected override void OnRelease()
        {
            transformModelObserver.OnDirectionChanged -= OnDirectionChanged;
            Model.OnControlStateChanged -= OnControlStateChanged;
        }
        
        private void OnControlStateChanged(PlayerControlState controlState)
        {
            bool canExecute = controlState == PlayerControlState.AirCraft;
            airCraftMonoBehaviour.UpdateState(canExecute);
        }

        private void OnDirectionChanged(Vector3 direction)
        {
            airCraftMonoBehaviour.UpdateState(direction.z, 0, direction.x);
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