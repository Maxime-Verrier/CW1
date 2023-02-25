using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AController : MonoBehaviour, IInteractable
{
    [SerializeField] public float interactRange = 15;

    public float Range => interactRange;

    public abstract void onDisabled();

    public void OnEndHover()
    {
    }

    public void OnInteract(Player player)
    {
        player.SwapController(this);
    }

    public void OnStartHover()
    {
    }
}
