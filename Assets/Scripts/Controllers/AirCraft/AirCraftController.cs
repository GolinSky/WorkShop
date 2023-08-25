using LightWeightFramework.Controller;
using WorkShop.Commands.AirCraft;
using WorkShop.LightWeightFramework.Command;
using WorkShop.Models.AirCraft;

namespace WorkShop.Controllers.AirCraft
{
    public class AirCraftController:Controller<AirCraftModel>
    {
        public AirCraftController(AirCraftModel model) : base(model)
        {
        }

        public override ICommand GetCommand()
        {
            return new AirCraftCommand(this, GameObserver);
        }
    }
}