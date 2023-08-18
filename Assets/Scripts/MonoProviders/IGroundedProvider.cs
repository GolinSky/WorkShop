using WorkShop.LightWeightFramework.MonoProviders;

namespace WorkShop.MonoProviders
{
    public interface IGroundedProvider : IMonoProvider
    {
        bool IsGrounded { get; }
    }

}