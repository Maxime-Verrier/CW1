using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AController : MonoBehaviour, IInteractable
{
    [SerializeField] private float interactionRange = 0;
    [SerializeField] private float interactRange = 15;

    public virtual float Range => interactRange;
    public virtual float InteractionRange => interactionRange;

    public abstract void onDisabled();

    public void OnEndHover()
    {
    }

    public virtual void OnInteract(Player player)
    {
        player.SwapController(this);
    }

    public void OnStartHover()
    {
    }
}
