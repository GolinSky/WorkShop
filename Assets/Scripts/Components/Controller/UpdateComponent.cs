using WorkShop.LightWeightFramework.Components;
using WorkShop.LightWeightFramework.Game;
using WorkShop.LightWeightFramework.Service;
using WorkShop.LightWeightFramework.UpdateService;

namespace WorkShop.Components.Controller
{
    public class UpdateComponent:Component 
    {
        private ITickService tickService;
        private ITick Tick { get; }

        public UpdateComponent(ITick tick)
        {
            Tick = tick;
        }
        protected override void OnInit(IGameObserver gameObserver)
        {
            tickService = gameObserver.ServiceHub.Get<ITickService>();
            tickService.AddObserver(Tick);
        }

        protected override void OnRelease()
        {
            tickService.RemoveObserver(Tick);
        }
    }

    
}