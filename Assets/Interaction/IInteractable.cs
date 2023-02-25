
public interface IInteractable
{
    float Range { get;  }

    void OnStartHover();
    void OnInteract();
    void OnEndHover();
}