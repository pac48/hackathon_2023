using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float acceleration = 2f;
    public float deceleration = 4f;

    private Rigidbody2D rb;

    private bool reverse = false;
    private float currentSpeed = 0f;


    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move forward/backward
        if (Input.GetKey(KeyCode.W))
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, moveSpeed, acceleration * Time.deltaTime);
            rb.velocity = transform.up * currentSpeed;
            reverse = false;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, -moveSpeed, acceleration * Time.deltaTime);
            rb.velocity = transform.up * currentSpeed;
            reverse = true;
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
            rb.velocity = transform.up * currentSpeed;
        }

        // Rotate left/right if moving
        if(currentSpeed != 0f)
        {
            if (Input.GetKey(KeyCode.A))
            {
                if(reverse) {
                    transform.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);
                } else {
                    transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                if(reverse) {
                    transform.Rotate(-Vector3.back * rotationSpeed * Time.deltaTime);
                } else {
                    transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
                }
            }
        }

        
    }
}
