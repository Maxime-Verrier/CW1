using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private AController mainController = default;
    [SerializeField] public CameraFollow camera = default;

    private AController currentController = null;

    public void Start()
    {
        currentController = this.mainController;
        currentController.gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public void SwapController(AController controller)
    {
        currentController.gameObject.layer = LayerMask.NameToLayer("Interactable");
        currentController.enabled = false;
        currentController.onDisabled();
        controller.gameObject.layer = LayerMask.NameToLayer("Default");
        controller.enabled = true;
        currentController = controller;
        camera.target = controller.transform;
    }
}