/*
 * Player Controller
 * 
 * Unity script for control basic movement and orientation of player
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Public Variables
    public float speed;                 // Speed of player
    public float rotation;              // Orientation of player
    public float acceleration;          // Linear acceleration of player
    public float angular;               // Angular acceleration of player

    // Set max speed for player
    public float maxSpeed = 20;

    private Vector3 velocity;

    void FixedUpdate()
    {
        // Get input from user. This is just here test movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        velocity = new Vector3(moveHorizontal * speed, moveVertical * speed, 0);

        // Update position using simple Euler method
        transform.position = transform.position + velocity * Time.fixedDeltaTime;

        transform.eulerAngles = new Vector3(0, 0, GetNewOrientation(transform.rotation.eulerAngles.z, velocity));

        // Update linear velocity
        if (moveHorizontal == 0 && moveVertical == 0)
        {
            speed = 0;
        }
        else
        {
            speed = speed + acceleration * Time.deltaTime;

            if (speed > maxSpeed)
            {
                speed = maxSpeed;
            }
        }
   

    }

    // Force the orientation of the character to be in the direction that it is moving
    float GetNewOrientation(float currentOrientation, Vector3 velocity)
    {

        // Make sure we have a velocity
        if (velocity.magnitude > 0)
        {
            // Calculate orientation using an arctan of the velocity components
            return Mathf.Atan2(-velocity.x, velocity.y) * (180 / (Mathf.PI));
        }
        else
        {
            // Otherwise return to current orientation
            return currentOrientation;
        }

    }

    // Return velocity of player
    public Vector3 GetVelocity()
    {

        return velocity;

    }
}

