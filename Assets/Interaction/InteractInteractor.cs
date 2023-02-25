using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractInteractor : MonoBehaviour, IInteractable
{
    [SerializeField] public float range = 1;
    [SerializeField] public string hoverMessage = null;
    [SerializeField] public GameObject interactor = default;

    public float Range => range;

    public void OnEndHover()
    {
    }

    public void OnInteract()
    {
        if (interactor != null)
        {
            IInteractor interc = interactor.GetComponent<IInteractor>();

            if (interc != null) interc.OnInteract();
        }
    }

    public void OnStartHover()
    {
    }
}
