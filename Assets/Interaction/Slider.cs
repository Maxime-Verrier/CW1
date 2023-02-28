using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour, IInteractor
{
    [SerializeField] private Vector3[] targets;
    [SerializeField] private float speed = 1;

    private int targetIndex = 0;
    private bool moving = false;

    void Update()
    {
        if (moving)
        {
            Vector3 target = targets[targetIndex];
            if (Vector3.Distance(transform.rotation.eulerAngles, targets[targetIndex]) < 0.1f)
            {
                targetIndex = targetIndex >= targets.Length - 1 ? 0 : targetIndex + 1;
                moving = false;
            }

            else transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(target), Time.deltaTime * speed);
        }
    }

    public void OnInteract(Player player)
    {
        moving = !moving;
    }
}
