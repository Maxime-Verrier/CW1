using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    private Vector2 angle = new Vector2(45f, 0f);
    float lastManualRotationTime;

    [SerializeField]
    public Transform target = default;

    [SerializeField]
    public float pLerp = .02f;

    [SerializeField]
    public float rLerp = .01f;

    [SerializeField]
    public float offset = 0;

    [SerializeField, Range(1f, 360f)]
    float rotationSpeed = 90f;

    [SerializeField, Range(-89f, 89f)]
    float minVerticalAngle = -30f, maxVerticalAngle = 60f;

    [SerializeField]
    float minZoom = 3f;

    [SerializeField]
    float maxZoom = 8f;

    [SerializeField]
    public float zoomSpeed = 1;


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
            lookRotation = transform.localRotation;
        }
        Vector3 focusPoint = target.position;
        Vector3 lookDirection = lookRotation * Vector3.forward;
        Vector3 lookPosition = Vector3.Lerp(transform.position, focusPoint - lookDirection * offset, pLerp);
        transform.SetPositionAndRotation(lookPosition, lookRotation);
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

        if (zoom != 0) offset = Mathf.Max(Mathf.Min(offset + zoom, maxZoom), minZoom);
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
}