using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    Rigidbody rigidBody;
    const float angle = 25;
    float xAxis, yAxis, zAxis;
    float xAngle, yAngle, zAngle = 0;
    float previousPitch = 0;
    Quaternion offset;

    [SerializeField]
    float speed = 1;

    [SerializeField]
    float maxSpeed = 1;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        UserInput();
        transform.localEulerAngles = Vector3.back * xAngle + Vector3.right * zAngle + yAngle * Vector3.up;
    }

    private void FixedUpdate()
    {
        rigidBody.AddForce(xAxis, yAxis, zAxis);
        rigidBody.velocity = Vector3.ClampMagnitude(Vector3.Lerp(rigidBody.velocity, Vector3.zero, Time.deltaTime), maxSpeed);
    }

    void UserInput()
    {
        if (Input.GetKey(KeyCode.C))
        {
            yAngle = Mathf.Lerp(yAngle, 360, Time.deltaTime);
        }
        else
        {
            yAngle = Mathf.Lerp(yAngle, 0, Time.deltaTime);
        }
        // Y Axis input
        if (Input.GetKey(KeyCode.Space))
        {
            yAxis = speed;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            yAxis = -1;
        }
        else
        {
            yAxis = 0;
        }

        // Forward/Z Axis Angle/Axis input
        if (Input.GetKey(KeyCode.W))
        {
            zAngle = Mathf.Lerp(zAngle, angle, Time.deltaTime);
            zAxis = speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            zAngle = Mathf.Lerp(zAngle, -angle, Time.deltaTime);
            zAxis = -speed;
        }
        else
        {
            zAngle = Mathf.Lerp(zAngle, 0, Time.deltaTime);
            zAxis = 0;
        }

        // Left/Right or X axis/angle input
        if (Input.GetKey(KeyCode.Q))
        {
            xAngle = Mathf.Lerp(xAngle, -angle, Time.deltaTime);
            xAxis = -speed;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            xAngle = Mathf.Lerp(xAngle, angle, Time.deltaTime);
            xAxis = speed;
        }
        else
        {
            xAngle = Mathf.Lerp(xAngle, 0, Time.deltaTime);
            xAxis = 0;
        }

        // Rotation From Mouse
    }
}
