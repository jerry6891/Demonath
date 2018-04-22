using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jump : MonoBehaviour {
    public float jumpSpeed = 5f;
    public float disToGround = 0.5f;
    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
         //rb.AddForce(0, 2, 0);
        Debug.Log(isGrounded());
        if (Input.GetKey(KeyCode.Space) && isGrounded())
        {
            // rb.AddForce(0, 4, 0);
            Vector3 jumpVelocity = new Vector3(0f, jumpSpeed, 0f);
            rb.velocity = rb.velocity + jumpVelocity;
        }
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, disToGround);
    }
}
