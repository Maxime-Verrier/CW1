using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public float pLerp = .02f;
    public float rLerp = .01f;
    public Vector3 offset = Vector3.zero;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, Time.deltaTime);
       // transform.rotation = Quaternion.Lerp(transform.rotation, target.transform.rotation, rLerp);
    }
}
