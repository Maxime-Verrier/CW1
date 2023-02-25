using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasting : MonoBehaviour
{
    [SerializeField] private Transform body;
    [SerializeField] public Camera camera = null;
    [SerializeField] private LayerMask mask;

    private IInteractable target;

    private void Update()
    {
        RaycastInteractable();

        if (target != null && Input.GetKeyDown(KeyCode.F))
        {
            target.OnInteract();
        }
    }

    private void RaycastInteractable()
    {
        RaycastHit hitted;
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        Debug.DrawRay(ray.origin, 100 * ray.direction);
        if (Physics.Raycast(ray, out hitted, Mathf.Infinity, mask))
        {
            IInteractable interactable = hitted.collider.GetComponent<IInteractable>();

            print(interactable);
            if (interactable == null || Vector3.Distance(hitted.transform.position, body.position) > interactable.Range)
            {
                if (target != null)
                {
                    target.OnEndHover();
                    target = null;
                }
            }
            else
            {
                if (interactable == target) return;
                else if (target != null)
                {
                    target.OnEndHover();
                }
                target = interactable;
                target.OnStartHover();
            }
        }
        else if (target != null)
        {
            target.OnEndHover();
            target = null;
        }
    }
}