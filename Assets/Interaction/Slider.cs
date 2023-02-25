using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour, IInteractor
{
    [SerializeField] public float angle = 90;

    public void OnInteract()
    {
        transform.Rotate(Vector3.up, angle);
    }
}
