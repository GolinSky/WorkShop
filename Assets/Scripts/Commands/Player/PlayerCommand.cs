using WorkShop.Controllers;
using WorkShop.LightWeightFramework.Command;
using WorkShop.LightWeightFramework.Game;
using WorkShop.MonoProviders;
using WorkShop.Services.Player;

namespace WorkShop.Commands.Player
{
    public interface IPlayerCommand:ICommand
    {
        void RegisterMonoProvider(IMovementProvider movementProvider);

    }
    public class PlayerCommand:Command<PlayerController>, IPlayerCommand
    {
        private readonly IActorTransformService actorTransformService;
        public PlayerCommand(PlayerController controller, IGameObserver observer) : base(controller, observer)
        {
            actorTransformService = observer.ServiceHub.Get<IActorTransformService>();
        }
        
        public void RegisterMonoProvider(IMovementProvider movementProvider)
        {
            actorTransformService.AddActor(ActorType.Player, movementProvider);
        }
    }
}