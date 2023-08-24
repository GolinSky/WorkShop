using WorkShop.Controllers;
using WorkShop.LightWeightFramework.Command;
using WorkShop.LightWeightFramework.Game;
using WorkShop.Services.Player;

namespace WorkShop.Commands.Player
{
    public interface IPlayerCommand:ICommand
    {
        IActorTransformCommand ActorTransformCommand { get; }

    }
    public class PlayerCommand:Command<PlayerController>, IPlayerCommand
    {
        public IActorTransformCommand ActorTransformCommand { get; }
        
        public PlayerCommand(PlayerController controller, IGameObserver observer) : base(controller, observer)
        {
            ActorTransformCommand = new ActorTransformCommand(controller, observer, ActorType.Player);
        }
    }
}