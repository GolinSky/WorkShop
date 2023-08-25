using WorkShop.Controllers.AirCraft;
using WorkShop.LightWeightFramework.Command;
using WorkShop.LightWeightFramework.Game;

namespace WorkShop.Commands.AirCraft
{
    public interface IAirCraftCommand:ICommand
    {
        
    }
    public class AirCraftCommand:Command<AirCraftController>, IAirCraftCommand
    {
        public AirCraftCommand(AirCraftController controller, IGameObserver gameObserver) : base(controller, gameObserver)
        {
        }
    }
}