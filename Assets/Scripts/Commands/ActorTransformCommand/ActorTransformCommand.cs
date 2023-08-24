using LightWeightFramework.Controller;
using WorkShop.LightWeightFramework.Command;
using WorkShop.LightWeightFramework.Game;
using WorkShop.MonoProviders;
using WorkShop.Services.Player;

namespace WorkShop.Commands
{
    public interface IActorTransformCommand
    {
        void RegisterMonoProvider(IMovementProvider movementProvider);

    }
    public class ActorTransformCommand:Command, IActorTransformCommand
    {
        public ActorType ActorType { get; }
        private readonly IActorTransformService actorTransformService;

        public ActorTransformCommand(IController controller, IGameObserver observer, ActorType actorType) : base(controller)
        {
            ActorType = actorType;
            actorTransformService = observer.ServiceHub.Get<IActorTransformService>();
        }

        public void RegisterMonoProvider(IMovementProvider movementProvider)
        {
            actorTransformService.AddActor(ActorType, movementProvider);
        }
    }
}