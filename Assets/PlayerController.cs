using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public bool isGrounded;
    public float jumpForce = 2.0f;
    public Rigidbody rbPlayer;
    public Vector3 vJump;

    // Use this for initialization
    void Start()
    {
		rbPlayer = GetComponent<Rigidbody>();
		vJump = new Vector3(0f, jumpForce, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        HandlePlayerMovement();
    }

	private void OnCollisionStay(Collision other)
	{
		isGrounded = true;
	}

    private void HandlePlayerMovement()
    {
        // WSAD or Arrow Keys
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3f;

        transform.Rotate(0, x, 0);
        transform.Translate(x,0, z);

		// Enables fps player to jump
		if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
			rbPlayer.AddForce(vJump * jumpForce, ForceMode.Impulse);
			isGrounded = false;
		}

    }

}
