using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public bool isGrounded;
    public float walkSpeed = 2f;
    public Rigidbody rbPlayer;
    public Vector3 jump;
    public float jumpForce = 2.0f;

    // Use this for initialization
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
        jump = new Vector3(0f, jumpForce, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
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
        rbPlayer.MovePosition(rbPlayer.position + moveDirection * walkSpeed * Time.deltaTime);
    }

    // Handles character jump behavior
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rbPlayer.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }
}
