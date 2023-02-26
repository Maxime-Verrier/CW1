using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dataset : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string data = "Master";

    public float Range => 3;

    public void OnEndHover()
    {
    }

    public void OnInteract(Player player)
    {
        player.key = data;
    }

    public void OnStartHover()
    {
    }
}
