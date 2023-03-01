using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour, IInteractor
{
    [SerializeField] private Vector3[] targets;
    [SerializeField] private float speed = 1;

    private int targetIndex = 0;
    private bool moving = false;

    void Update()
    {
        if (moving)
        {
            if (Vector3.Distance(transform.position, targets[targetIndex]) < 0.01f)
            {
                targetIndex = targetIndex >= targets.Length - 1 ? 0 : targetIndex + 1;
                moving = false;
            }

            else transform.position = Vector3.MoveTowards(transform.position, targets[targetIndex], speed * Time.deltaTime);
        }
    }

    public void OnInteract(Player player)
    {
        moving = !moving;
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit(Collision collision)
    {
        collision.transform.SetParent(null);
    }
}
