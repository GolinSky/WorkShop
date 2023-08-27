using WorkShop.Controllers;
using WorkShop.LightWeightFramework.Command;
using WorkShop.LightWeightFramework.Game;
using WorkShop.Services.Interaction;
using WorkShop.Services.Player;
using WorkShop.ViewComponents;

namespace WorkShop.Commands.Player
{
    public interface IPlayerCommand:ICommand
    {
        IActorTransformCommand ActorTransformCommand { get; }
        ITickCommand TickCommand { get;}

        void RegisterInteractable(IInteractable interactable);

    }
    public class PlayerCommand:Command<PlayerController>, IPlayerCommand
    {
        private IInteractionService interactionService;
        public IActorTransformCommand ActorTransformCommand { get; }
        public ITickCommand TickCommand { get; }
     

        public PlayerCommand(PlayerController controller, IGameObserver observer) : base(controller, observer)
        {
            ActorTransformCommand = new ActorTransformCommand(controller, observer, ActorType.Player);
            TickCommand = new TickCommand(controller, observer);
            interactionService = GameObserver.ServiceHub.Get<IInteractionService>();
        }
        
        public void RegisterInteractable(IInteractable interactable)
        {
            interactionService.AddInteractable(interactable);
        }
    
    }
}