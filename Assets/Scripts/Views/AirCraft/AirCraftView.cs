using WorkShop.Commands.AirCraft;
using WorkShop.LightWeightFramework;
using WorkShop.Models.AirCraft;

namespace WorkShop.Views
{
    public class AirCraftView:View<IAirCraftModelObserver, IAirCraftCommand>
    {
        protected override void OnInit(IAirCraftModelObserver model)
        {
            
        }

        protected override void OnRelease()
        {
        }

        protected override void OnCommandSet(IAirCraftCommand command)
        {
        }
    }
}