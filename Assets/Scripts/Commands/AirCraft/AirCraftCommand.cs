using UnityEngine;
using WorkShop.Controllers.AirCraft;
using WorkShop.LightWeightFramework.Command;
using WorkShop.LightWeightFramework.Game;
using WorkShop.ViewComponents;

namespace WorkShop.Commands.AirCraft
{
    public interface IAirCraftCommand:ICommand
    {
        IInteractable Interactable { get; }
        void SetPlayerSit(Transform playerSitTransform);
    }
    public class AirCraftCommand:Command<AirCraftController>, IAirCraftCommand
    {
        public AirCraftCommand(AirCraftController controller, IGameObserver gameObserver) : base(controller, gameObserver)
        {
        }
        

        public IInteractable Interactable => Controller;

        public void SetPlayerSit(Transform playerSitTransform)
        {
            Controller.SetPlayerSitTransform(playerSitTransform);
        }
    }
}