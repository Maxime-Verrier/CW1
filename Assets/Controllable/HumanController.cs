using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : AController
{
    private Rigidbody rigidBody;
    private Animator animator;
    private Vector3 movement;
    private bool grounded = false;

    [SerializeField]
    private float groundDrag = 5;

    [SerializeField]
    private float humanHeight = 2;

    [SerializeField]
    private float airMultiplier = 0.75f;

    [SerializeField]
    private float jumpCD = 0.75f;
    private bool jumpping = false;

    [SerializeField]
    private float jumpForce = 0.75f;


    [SerializeField]
    private float speed = 1;

    [SerializeField]
    LayerMask groundLayers = default;


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
        grounded = Physics.Raycast(transform.position, Vector3.down, 0.1f, groundLayers);
        Debug.DrawRay(transform.position, Vector3.down * 0.1f);
        rigidBody.drag = grounded ? groundDrag : 0;
    }

    protected void FixedUpdate()
    {
        Vector3 force = this.transform.forward * movement.z + this.transform.right * movement.x;

        if (grounded)
            rigidBody.AddForce(force.normalized * speed * 10f, ForceMode.Force);
        // In air
        else
            rigidBody.AddForce(force.normalized * speed * 10f * airMultiplier, ForceMode.Force);

        animator.SetBool("isMoving", force.x + force.y + force.z != 0);

        Vector3 currentVel = new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);

        if (currentVel.magnitude > speed)
        {
            Vector3 cappedVel = currentVel.normalized * speed;
            rigidBody.velocity = new Vector3(cappedVel.x, rigidBody.velocity.y, cappedVel.z);
        }
    }

    void UserInput()
    {

        if (grounded && !jumpping && Input.GetKey(KeyCode.Space))
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);
            rigidBody.AddForce(jumpForce * transform.up, ForceMode.Impulse);
            jumpping = true;
            Invoke(nameof(JumpReset), jumpCD);
        }

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
    }

    private void JumpReset()
    {
        jumpping = false;
    }
}