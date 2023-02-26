using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class RayCasting : MonoBehaviour
{
    [SerializeField] private LayerMask mask;

    private IInteractable target;
    public Player player = default;

    private void Start()
    {
        this.player = GetComponent<Player>();
    }

    private void Update()
    {
        RaycastInteractable();

        if (target != null && Input.GetKeyDown(KeyCode.F))
        {
            target.OnInteract(player);
        }
    }

    private void RaycastInteractable()
    {
        RaycastHit hitted;
        Ray ray = new Ray(player.camera.transform.position, player.camera.transform.forward);
        Debug.DrawRay(ray.origin, 3 * ray.direction);
        if (Physics.Raycast(ray, out hitted, Mathf.Infinity, mask))
        {
            IInteractable interactable = hitted.collider.GetComponent<IInteractable>();
            if (interactable == null || Vector3.Distance(hitted.transform.position, player.getCurrentController().transform.position) > interactable.Range)
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