/*
 * Lure Controller
 * 
 * Unity script for control basic movement of lure
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LureController : MonoBehaviour
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

        velocity = new Vector3(-speed, 0, 0);

        // Update position using simple Euler method
        transform.position = transform.position + velocity * Time.fixedDeltaTime;

        // Update linear velocity
        if (Input.GetKey("space"))
        {
            Debug.Log("Ping");
            speed = speed + acceleration * Time.deltaTime;

            if (speed > maxSpeed)
            {
                speed = maxSpeed;
            }
        }
        else
        {
            Debug.Log("Pong");
            speed = 0;
        }


    }

    // Return velocity of player
    public Vector3 GetVelocity()
    {

        return velocity;

    }
}
