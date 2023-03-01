using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField]
    private GameObject interactor;

    private bool activate = false;


    private void OnCollisionEnter(Collision collision)
    {
        if (!activate)
        {
            IInteractor interact = interactor.GetComponent<IInteractor>();

            interact.OnInteract(null);
            activate = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        activate = false;
    }
}
