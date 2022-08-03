namespace TomGustin.Interface
{
    public interface IInteractable
    {
        void Interact(PlayerController controller);
        InteractableType GetInteractType();
    }

    public enum InteractableType { Chat, Camera, None }
}