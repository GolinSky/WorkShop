using UnityEngine;
using WorkShop.Controllers;
using WorkShop.LightWeightFramework.Command;
using WorkShop.LightWeightFramework.Game;
using WorkShop.MonoProviders;
using WorkShop.Services.Player;

namespace WorkShop.Commands.Player
{
    public interface IPlayerCommand:ICommand
    {
        void UpdatePosition(Vector3 position);
        void RegisterMonoProvider(IGroundedProvider groundedProvider);

    }
    public class PlayerCommand:Command<PlayerController>, IPlayerCommand
    {
        private readonly IPlayerService playerService;
        public PlayerCommand(PlayerController controller, IGameObserver observer) : base(controller, observer)
        {
            playerService = observer.ServiceHub.Get<IPlayerService>();
        }

        public void UpdatePosition(Vector3 position)
        {
            playerService.UpdatePosition(position);
        }

        public void RegisterMonoProvider(IGroundedProvider groundedProvider)
        {
            Controller.RegisterGroundedProvider(groundedProvider);
        }
    }
}