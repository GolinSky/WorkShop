namespace WorkShop.ViewComponents
{
    public interface IInteractable
    {
        void TryInteract(); //no receiver/context
    }

    public interface IInteractableProvider
    {
        IInteractable GetInteractable();
    }
}