using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Public Variables
    public float speed;
    public float rotation;
    public float acceleration;
    public float angular;

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

    float GetNewOrientation(float currentOrientation, Vector3 velocity)
    {

        if (velocity.magnitude > 0)
        {
            return Mathf.Atan2(-velocity.x, velocity.y) * (180 / (Mathf.PI));
        }
        else
        {
            return currentOrientation;
        }

    }

    public Vector3 GetVelocity()
    {

        return velocity;

    }
}

