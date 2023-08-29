using UnityEngine;
using WorkShop.Commands.Player;
using WorkShop.LightWeightFramework.Command;
using WorkShop.LightWeightFramework.UpdateService;
using WorkShop.LightWeightFramework.ViewComponents;
using WorkShop.Models;
using WorkShop.Services.Player;

namespace WorkShop.ViewComponents
{
    public class InteractionViewComponent:ViewComponent<IPlayerModelObserver>, ICommandInvoker, ITick
    {
        [SerializeField] private float radius;
        [SerializeField] private LayerMask interactionLayerMask;
        
        private IPlayerCommand playerCommand;
        private Collider[] results = new Collider[1];
        private bool canExecute;

        public void SetCommand(ICommand command)
        {
            playerCommand = (IPlayerCommand)command;
            playerCommand.TickCommand.AddTickable(this);
        }

        protected override void OnInit()
        {
            base.OnInit();
            Model.OnControlStateChanged += UpdateControlState;
        }

        protected override void OnRelease()
        {
            playerCommand.TickCommand.RemoveTickable(this);
        }
        
        private void UpdateControlState(PlayerControlState controlState)
        {
            canExecute = controlState != PlayerControlState.ThirdPerson;
        }

        public void Notify(float state)
        {
            if(canExecute) return;
            
            int numColliders = Physics.OverlapSphereNonAlloc(transform.position, radius, results, interactionLayerMask);

            if (numColliders > 0)
            {
                for (var i = 0; i < numColliders; i++)
                {
                    Debug.DrawLine(transform.position, results[i].transform.position, Color.red);

                    var interactable = results[i].GetComponent<IInteractableProvider>();
                    if (interactable != null)
                    {
                        playerCommand.RegisterInteractable(interactable.GetInteractable());
                    }
                }
            }
            
        }
        
    }
}