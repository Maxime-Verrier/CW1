using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : InteractInteractor
{
    [SerializeField] private string key = null;

    public override void OnInteract(Player player)
    {
        if (key == "" ||key == null || player.key == key || player.key == "Master") base.OnInteract(player);
        else
        {
            display.text = key + "'s data required";
        }
    }
}