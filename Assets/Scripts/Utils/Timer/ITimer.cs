namespace WorkShop.Utils.Timer
{
    public interface ITimer
    {
        void SetTimer(float delay);
        bool IsFinished { get; }
        void Start();
    }
}