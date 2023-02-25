
public interface IInteractable
{
    float Range { get;  }

    void OnStartHover();
    void OnInteract(Player player);
    void OnEndHover();
}