using UnityEngine;
using System.Collections;
     


public class CameraFollow : MonoBehaviour
{
    public Transform target; // The target to follow
    public float smoothSpeed = 0.125f; // The smoothing factor for camera movement
    public Vector2 offset; // The offset from the target's position

    void LateUpdate()
    {
        // Calculate the camera's new position based on the target's position
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}