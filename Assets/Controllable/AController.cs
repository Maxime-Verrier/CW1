using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AController : MonoBehaviour, IInteractable
{

    public Vector2 angle = new Vector2(45f, 0f);
    float lastManualRotationTime;
    protected Camera camera;

    [SerializeField]
    public float pLerp = .02f;

    [SerializeField]
    public float rLerp = .01f;

    [SerializeField]
    public Vector3 offset = Vector3.zero;

    [SerializeField, Range(1f, 360f)]
    float rotationSpeed = 90f;

    [SerializeField, Range(-89f, 89f)]
    public float minVerticalAngle = -30f, maxVerticalAngle = 60f;

    [SerializeField]
    public float minZoom = 3f;

    [SerializeField]
    public float maxZoom = 8f;

    [SerializeField]
    public float zoomSpeed = 1;

    [SerializeField]
    private float interactionRange = 0;

    [SerializeField]
    private float interactRange = 15;

    public virtual float Range => interactRange;
    public virtual float InteractionRange => interactionRange;

    protected virtual void Start()
    {
        camera = Camera.main;
    }

    void LateUpdate()
    {
        ApplyZoom();
        Quaternion lookRotation;

        if (ManualRotation())
        {
            ResetAngles();
            lookRotation = Quaternion.Euler(angle); 

        }
        else
        {
            lookRotation = camera.transform.localRotation;
        }

        Vector3 lookPosition = Vector3.Lerp(transform.position + transform.forward * offset.z - transform.up * offset.y - transform.right * offset.x, transform.position, 0);
        camera.transform.SetPositionAndRotation(lookPosition, lookRotation);
        Vector3 controllerAngle = angle;
        controllerAngle.x = this.transform.eulerAngles.x;
        this.transform.rotation = Quaternion.Euler(controllerAngle);
    }

    void OnValidate()
    {
        if (maxVerticalAngle < minVerticalAngle)
        {
            maxVerticalAngle = minVerticalAngle;
        }
    }

    void ApplyZoom()
    {
        float zoom = Input.GetAxis("Mouse Scroll") * zoomSpeed;

        //if (zoom != 0) offset = Mathf.Max(Mathf.Min(offset + zoom * Vector3.forward, maxZoom), minZoom);
    }

    bool ManualRotation()
    {
        Vector2 input = new Vector2(
            Input.GetAxis("Mouse Y"),
            Input.GetAxis("Mouse X")
        );
        const float e = 0.001f;
        if (input.x < -e || input.x > e || input.y < -e || input.y > e)
        {
            angle += rotationSpeed * Time.unscaledDeltaTime * input;
            lastManualRotationTime = Time.unscaledTime;
            return true;
        }
        return false;
    }
    void ResetAngles()
    {
        angle.x = Mathf.Clamp(angle.x, minVerticalAngle, maxVerticalAngle);

        if (angle.y < 0f)
        {
            angle.y += 360f;
        }
        else if (angle.y >= 360f)
        {
            angle.y -= 360f;
        }
    }

    public abstract void onDisabled();

    public void OnEndHover()
    {
    }

    public virtual void OnInteract(Player player)
    {
        player.SwapController(this);
    }

    public void OnStartHover()
    {
    }
}
