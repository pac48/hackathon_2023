using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;

    private Rigidbody2D rb;

    private bool canTurn = false;
    private bool reverse = false;

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
            rb.velocity = transform.up * moveSpeed;
            canTurn = true;
            reverse = false;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = -transform.up * moveSpeed;
            canTurn = true;
            reverse = true;
        }
        else
        {
            rb.velocity = Vector2.zero;
            canTurn = false;
        }

        // Rotate left/right if moving
        if (canTurn)
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
