using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    private Rigidbody rigidBody;
    private const float angle = 0.069f;
    private const float tiltSpeed = 280;
    private float xAngle, yAngle, zAngle = 0;
    private Vector3 movement;
    private Vector3 rotation;

    [SerializeField]
    float speed = 1;

    [SerializeField]
    float rotationSpeed = 1;

    [SerializeField]
    float maxSpeed = 1;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        UserInput();
//        transform.localEulerAngles = Vector3.back * xAngle + Vector3.right * zAngle + yAngle * Vector3.up;
    }

    private void FixedUpdate()
    {
        print(rotation);
        Vector3 force = this.transform.forward * movement.z + this.transform.right * movement.x;

        // This code allow us to ignore the Y relative axis while moving, as the play only move relative to the drone rotation on x/z 
        force.y = movement.y;

        rigidBody.AddForce(force * speed * Time.deltaTime);

        rigidBody.rotation = Quaternion.Euler(new Vector3(rotation.x * tiltSpeed, rotation.y * rotationSpeed, rotation.z * tiltSpeed));
        rigidBody.velocity = Vector3.ClampMagnitude(Vector3.Lerp(rigidBody.velocity, Vector3.zero, Time.deltaTime), maxSpeed);
    }

    void UserInput()
    {
        rotation.y += Input.GetKey(KeyCode.Q) ? -Time.deltaTime : (Input.GetKey(KeyCode.E) ? Time.deltaTime: 0);
        movement.y = Input.GetKey(KeyCode.Space) ? 1 : (Input.GetKey(KeyCode.LeftShift) ? -1 : 0);

        // Forward/Z Axis Angle/Axis input
        if (Input.GetKey(KeyCode.W))
        {
            rotation.x = Mathf.Lerp(rotation.x, angle, Time.deltaTime);
            movement.z = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rotation.x = Mathf.Lerp(rotation.x, -angle, Time.deltaTime);
            movement.z = -1;
        }
        else
        {
            rotation.x = Mathf.Lerp(rotation.x, 0, Time.deltaTime);
            movement.z = 0;
        }

        // Forward/Z Axis Angle/Axis input
        if (Input.GetKey(KeyCode.A))
        {
            rotation.z = Mathf.Lerp(rotation.z, angle, Time.deltaTime);
            movement.x = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotation.z = Mathf.Lerp(rotation.z, -angle, Time.deltaTime);
            movement.x = 1;
        }
        else
        {
            rotation.z = Mathf.Lerp(rotation.z, 0, Time.deltaTime);
            movement.x = 0;
        }
    }
}