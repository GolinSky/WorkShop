using LightWeightFramework.Controller;
using WorkShop.LightWeightFramework.Command;
using WorkShop.LightWeightFramework.Game;
using WorkShop.LightWeightFramework.Service;
using WorkShop.LightWeightFramework.UpdateService;

namespace WorkShop.Commands
{
    public interface ITickCommand : ICommand
    {
        void AddTickable(ITick tick);
        void RemoveTickable(ITick tick);
    }
    
    public class TickCommand:Command, ITickCommand
    {
        private readonly ITickService tickService;

        public TickCommand(IController controller, IGameObserver gameObserver) : base(controller)
        {
            tickService = gameObserver.ServiceHub.Get<ITickService>();
        }
        
        void ITickCommand.AddTickable(ITick tick)
        {
            tickService.AddObserver(tick);
        }

        void ITickCommand.RemoveTickable(ITick tick)
        {
            tickService.RemoveObserver(tick);
        }
        
        
    }
}