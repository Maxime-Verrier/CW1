using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dataset : MonoBehaviour, IInteractable
{
    public Text display;
    [SerializeField]
    private string data = "Master";

    public float Range => 3;

    public void OnEndHover()
    {
    }

    public void OnInteract(Player player)
    {
        display.text = data + "'s data aquired";
        player.key = data;
    }

    public void OnStartHover()
    {
    }
}
