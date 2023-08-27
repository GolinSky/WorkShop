namespace WorkShop.Strategy
{
    public interface IStrategy
    {
        
    }

    public interface IStrategyContext<in TStrategy> where TStrategy:IStrategy
    {
        void SetStrategy(TStrategy strategy);
    }
}