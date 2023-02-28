using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : AController
{
    private Rigidbody rigidBody;
    private const float tiltAngle = 0.069f;
    private const float tiltSpeed = 280;
    private Vector3 movement;
    private Vector3 rotation;

    [SerializeField]
    float speed = 1;

    [SerializeField]
    float angularSpeed = 1;

    [SerializeField]
    float maxSpeed = 1;

    protected override void Start()
    {
        Cursor.visible = false;
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
        base.Start();
    }

    void Update()
    {
        UserInput();
    }

    protected void FixedUpdate()
    {
        Vector3 force = this.transform.forward * movement.z + this.transform.right * movement.x;

        // This code allow us to ignore the Y relative axis while moving, as the play only move relative to the drone rotation on x/z 
        force.y = movement.y;

        rigidBody.AddForce(force * speed * Time.deltaTime);

//        transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x + rotation.x * tiltSpeed, transform.eulerAngles.y , transform.eulerAngles.z + rotation.z * tiltSpeed));
        rigidBody.velocity = Vector3.ClampMagnitude(Vector3.Lerp(rigidBody.velocity, Vector3.zero, Time.deltaTime), maxSpeed);
    }

    void UserInput()
    {
        rotation.y += Input.GetKey(KeyCode.Q) ? -Time.deltaTime : (Input.GetKey(KeyCode.E) ? Time.deltaTime: 0);
        movement.y = Input.GetKey(KeyCode.Space) ? 1 : (Input.GetKey(KeyCode.LeftShift) ? -1 : 0);

        // Forward/Z Axis Angle/Axis input
        if (Input.GetKey(KeyCode.W))
        {
            rotation.x = Mathf.Lerp(rotation.x, tiltAngle, Time.deltaTime);
            movement.z = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rotation.x = Mathf.Lerp(rotation.x, -tiltAngle, Time.deltaTime);
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
            rotation.z = Mathf.Lerp(rotation.z, tiltAngle, Time.deltaTime);
            movement.x = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotation.z = Mathf.Lerp(rotation.z, -tiltAngle, Time.deltaTime);
            movement.x = 1;
        }
        else
        {
            rotation.z = Mathf.Lerp(rotation.z, 0, Time.deltaTime);
            movement.x = 0;
        }
    }

    public override void onDisabled()
    {
        this.rigidBody.velocity = Vector3.zero;
    }
}