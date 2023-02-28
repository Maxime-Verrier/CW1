using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractInteractor : MonoBehaviour, IInteractable
{
    [SerializeField] public float range = 1;
    [SerializeField] public string hoverMessage = "Press F";
    [SerializeField] public GameObject interactor = default;
    [SerializeField] public Text display = default;

    public float Range => range;

    public void OnEndHover()
    {
        if (display != null) display.text = "";
    }

    public virtual void OnInteract(Player player)
    {
        if (interactor != null)
        {
            IInteractor interc = interactor.GetComponent<IInteractor>();

            if (interc != null) interc.OnInteract(player);
        }
    }

    public void OnStartHover()
    {
        if (display != null) display.text = hoverMessage;
    }
}
