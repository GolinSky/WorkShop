using LightWeightFramework.Controller;
using WorkShop.Controllers.Camera;
using WorkShop.LightWeightFramework.Command;
using WorkShop.LightWeightFramework.Game;
using WorkShop.Services.Player;

namespace WorkShop.Commands.Camera
{
    public interface ICameraCommand:ICommand
    {
        IActorTransformCommand ActorTransformCommand { get; }

    }
    
    public class CameraCommand:Command<CameraController>, ICameraCommand
    {
        public IActorTransformCommand ActorTransformCommand { get; }

        public CameraCommand(CameraController controller, IGameObserver observer) : base(controller, observer)
        {
            ActorTransformCommand = new ActorTransformCommand(controller, observer, ActorType.Camera);
        }

    }
}