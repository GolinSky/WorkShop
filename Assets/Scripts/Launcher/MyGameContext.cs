using WorkShop.LightWeightFramework.Game;
using WorkShop.LightWeightFramework.Service;
using WorkShop.Services.Cursor;
using WorkShop.Services.Player;

namespace WorkShop.Launcher
{
    public class MyGameContext : IGameContext
    {
        public IService[] Services => new IService[]
        {
            new InputService(),
            new PlayerService(),
            new CursorService()
        };
    }
}