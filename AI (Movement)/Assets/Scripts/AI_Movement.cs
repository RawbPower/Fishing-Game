using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Movement : MonoBehaviour
{

    // Public Variables
    public float velocity;
    public float rotation;
    public float acceleration;
    public float angular;

    void FixedUpdate()
    {
        // Get input from user. This is just here test movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Update position using simple Euler method
        transform.position = transform.position + new Vector3(moveHorizontal * velocity * Time.fixedDeltaTime, moveVertical * velocity * Time.fixedDeltaTime, 0);

        // Update angle
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + rotation * Time.fixedDeltaTime);

        if (transform.rotation.eulerAngles.z > 360)
        {
            transform.Rotate(0, 0, transform.rotation.eulerAngles.z - 360);
        }

        // Update linear velocity
        if (moveHorizontal == 0 && moveVertical == 0)
        {
            velocity = 0;
        } else {
            velocity = velocity + acceleration * Time.deltaTime;
        }

        // Update angular velocity
        rotation = rotation + angular * Time.fixedDeltaTime;
    }
}
