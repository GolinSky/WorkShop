using UnityEngine;
using WorkShop.Commands.Player;
using WorkShop.LightWeightFramework.Command;
using WorkShop.LightWeightFramework.UpdateService;
using WorkShop.LightWeightFramework.ViewComponents;
using WorkShop.Models;

namespace WorkShop.ViewComponents
{
    public class InteractionViewComponent:ViewComponent<IPlayerModelObserver>, ICommandInvoker, ITick
    {
        [SerializeField] private float radius;
        [SerializeField] private LayerMask interactionLayerMask;
        
        private IPlayerCommand playerCommand;
        private Collider[] results = new Collider[1];
        private bool canInteract;
        
        public void SetCommand(ICommand command)
        {
            playerCommand = (IPlayerCommand)command;
            playerCommand.TickCommand.AddTickable(this);
        }
        
        protected override void OnRelease()
        {
            playerCommand.TickCommand.RemoveTickable(this);
        }

        public void Notify(float state)
        {
            int numColliders = Physics.OverlapSphereNonAlloc(transform.position, radius, results, interactionLayerMask);

            if (numColliders > 0 && canInteract)
            {
                
            }
            for (var i = 0; i < numColliders; i++)
            {
                if (results[i] is IInteractable interactable)
                {
                   bool isSuccess = interactable.TryInteract();
                   if (isSuccess)
                   {
                       canInteract = false;
                       break;
                   }
                }
            }
        }
    }
}