using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : AController
{
    private Rigidbody rigidBody;
    private Animator animator;
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
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
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

        rigidBody.AddForce(force.normalized * speed, ForceMode.Force );

        animator.SetBool("isMoving", force.x + force.y + force.z != 0);
    }

    void UserInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            movement.z = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movement.z = -1;
        }
        else
        {
            movement.z = 0;
        }

        if (Input.GetKey(KeyCode.A))
        {
            movement.x = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movement.x = 1;
        }
        else
        {
            movement.x = 0;
        }
    }

    public override void onDisabled()
    {
        this.rigidBody.velocity = Vector3.zero;
    }
}