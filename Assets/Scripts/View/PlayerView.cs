using WorkShop.Commands.Player;
using WorkShop.LightWeightFramework.Views;
using WorkShop.Models;
using WorkShop.Models.TransformModels;
using WorkShop.MonoProviders;

namespace WorkShop.View
{
    public class PlayerView : View<IPlayerModelObserver, IPlayerCommand>
    {
        private IMovementProvider movementProvider;
        private ITransformModelObserver transformModelObserver;

        protected override void OnInit(IPlayerModelObserver model)
        {
            foreach (var viewComponent in viewComponents)
            {
                if (viewComponent is IMovementProvider groundedProvider)
                {
                    movementProvider = groundedProvider;
                    break;
                }
            }
        }

        protected override void OnRelease()
        {
        }

        protected override void OnCommandSet(IPlayerCommand command)
        {
            command.ActorTransformCommand.RegisterMonoProvider(movementProvider);
        }
    }
}