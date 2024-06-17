using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed at which the camera follow object moves down
    public float maxOffset = 2f; // Maximum offset the camera follow object can move down

    private Vector3 initialPosition;
    private float currentOffset = 0f;

    private void Start()
    {
        // Store the initial position of the CameraFollow object
        initialPosition = transform.localPosition;
    }

    private void Update()
    {
        // Check if the "s" key is held down
        if (Input.GetKey(KeyCode.S))
        {
            // Move the CameraFollow object down
            currentOffset = Mathf.Max(currentOffset - moveSpeed * Time.deltaTime, -maxOffset);
        }
        else
        {
            // Move the CameraFollow object back to its initial position
            currentOffset = Mathf.Min(currentOffset + moveSpeed * Time.deltaTime, 0f);
        }

        // Apply the current offset to the CameraFollow object's position
        transform.localPosition = new Vector3(initialPosition.x, initialPosition.y + currentOffset, initialPosition.z);
    }
}
