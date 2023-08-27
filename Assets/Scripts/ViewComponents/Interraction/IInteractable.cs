namespace WorkShop.ViewComponents
{
    public interface IInteractable
    {
        bool TryInteract(); //no receiver/context
    }

    public interface IInteractableProvider
    {
        IInteractable GetInteractable();
    }
}