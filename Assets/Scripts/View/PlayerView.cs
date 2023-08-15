using WorkShop.LightWeightFramework.Views;
using WorkShop.Models;

namespace WorkShop.View
{
    public class PlayerView: View<IPlayerModelObserver>
    {
        protected override void OnInit(IPlayerModelObserver model)
        {

        }
        
        protected override void OnRelease()
        {
            
        }
    }
}