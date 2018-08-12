using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public bool isRunning;
    public float walkSpeed = 3f;
    public float runSpeed = 10f;
    public Rigidbody rbPlayer;
    public bool isJumping;
    public Vector3 jump;
    public float jumpForce = 2.0f;
    public bool isGrounded;

    // Use this for initialization
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
        jump = new Vector3(0f, jumpForce, 0f);
    }


    /*
        Update() can vary out of step with the physics engine, either faster or slower, 
        depending on how much of a load the graphics are putting on the rendering 
        engine at any given time, which - if used for physics - would give 
        correspondingly variant physical effects!
     */
    void Update()
    {

        CallDefaultBehavior();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isJumping = true;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
    }

    /*
        FixedUpdate should be used when applying forces, torques, or 
        other physics-related functions - because you know it will 
        be executed exactly in sync with the physics engine itself.
        More on: https://stackoverflow.com/questions/34447682/what-is-the-difference-between-update-fixedupdate-in-unity
    */
    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void CallDefaultBehavior()
    {
        isRunning = false;
        isJumping = false;
    }

    private void OnCollisionStay(Collision other)
    {
        isGrounded = true;
    }

    // Handles character movement
    private void Move()
    {
        var moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        moveDirection.y = 0;

        if (isRunning)
        {
            rbPlayer.MovePosition(rbPlayer.position + moveDirection * runSpeed * Time.deltaTime);
        }
        else
        {
            rbPlayer.MovePosition(rbPlayer.position + moveDirection * walkSpeed * Time.deltaTime);
        }

    }

    // Handles character jump behavior
    private void Jump()
    {
        if (isJumping)
        {
            isJumping = !isJumping;
            isGrounded = false;
            rbPlayer.AddForce(jump * jumpForce, ForceMode.Impulse);
        }
    }
}
