namespace WorkShop.Utils.Timer
{
    public static class TimerFactory
    {
        public static ITimer GetTimer()
        {
            return new Timer();
        }
    }
}