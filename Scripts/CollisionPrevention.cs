using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPrevention : MonoBehaviour
{
    public GameObject otherPrefabTag; // The tag of the other prefab to prevent collisions with

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject == otherPrefabTag)
        {
            // Calculate the direction and magnitude of the collision
            Vector2 collisionDirection = col.contacts[0].point - (Vector2)transform.position;
            float collisionMagnitude = collisionDirection.magnitude;

            // Prevent the collision by adjusting the velocity of both prefabs
            float velocityMagnitude = rb.velocity.magnitude;
            rb.velocity = -collisionDirection.normalized * velocityMagnitude;
            col.gameObject.GetComponent<Rigidbody2D>().velocity = collisionDirection.normalized * velocityMagnitude;
        }
    }
}
