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
    private Vector3 prevPos;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {

        Vector3 mouseScreen = Input.mousePosition;
        Vector3 mouse = Camera.main.ScreenToWorldPoint(mouseScreen);

        //Debug.Log(Physics2D.OverlapCircle(new Vector2(mouse.x, mouse.y), 0.5f).gameObject.name);

        prevPos = transform.position;
        //Debug.Log(prevPos);
        // Get input from user. This is just here test movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        velocity = new Vector3(moveHorizontal * speed, moveVertical * speed, 0);

        // Update position using simple Euler method
        // transform.position = transform.position + velocity * Time.fixedDeltaTime;
        rb.velocity = new Vector2(velocity.x, velocity.y);

        rotation = GetNewOrientation(transform.rotation.eulerAngles.z, velocity);
        transform.eulerAngles = new Vector3(0, 0, rotation);
        //transform.rotation = Quaternion.AngleAxis(GetNewOrientation(transform.rotation.eulerAngles.z, velocity), Vector3.forward);

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
        Vector3 mouseScreen = Input.mousePosition;
        Vector3 mouse = Camera.main.ScreenToWorldPoint(mouseScreen);

        return Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg - 90;

        // Make sure we have a velocity
        /*if (velocity.magnitude > 0)
        {
            // Calculate orientation using an arctan of the velocity components
            return Mathf.Atan2(-velocity.x, velocity.y) * Mathf.Rad2Deg;
        }
        else
        {
            // Otherwise return to current orientation
            return currentOrientation;
        }*/

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OnCollisionEnter2D");
        //transform.position = transform.position - velocity * Time.fixedDeltaTime;
        //Debug.Log(transform.gameObject.name);
    }

    // Return velocity of player
    public Vector3 GetVelocity()
    {

        return velocity;

    }
}

