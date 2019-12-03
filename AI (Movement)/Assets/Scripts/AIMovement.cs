/*
 * AI Movement
 * 
 * MonoBehavior subclass to update the movement of AI
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AIMovement : MonoBehaviour
{

    [System.Serializable]
    public enum Movement { Seek, Flee, Arrive, Align, Pursue, Pursue2, Face, Look }         // Enum for different AI movement types

    // Public Variables

    // Max speed for character
    public float maxSpeed = 4;
    // Min speed. After this the character might as well have stopped. Because we only decrease acceleration it will keep decreasing 
    // smaller and smaller so we have to set it to zero at some point
    public double minSpeed;                   

    public float maxAcceleration = 1;                   // Max linear acceleration for character
    public float rotation = 0;                          // Rotation of character
    public Vector3 velocity = Vector3.zero;             // Velocity of character           

    public float timeToTarget = 0.1f;                   // Causes the movement to slow down as it gets close so that it does overshoot
    public GameObject target;                           // Game Object for target
    public Movement movement;                           // Movement enum

    public float targetRadius = 0.001f;                 // Radius at which the character is close enough to the target 
    public float slowRadius = 3.0f;                     // Radius at which the character will start to slow down

    private SteeringBehavior sb;                        // Variable for holding steering type

    // Set speed when called
    private void Awake()
    {
        minSpeed = 0.2 * Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Select movement type using subclasses of Steering Behavior
        switch (movement)
        {
            case Movement.Seek:
                sb = new Seek(this.gameObject, target, maxAcceleration);
                break;
            case Movement.Flee:
                sb = new Flee(this.gameObject, target, maxAcceleration);
                break;
            case Movement.Arrive:
                sb = new Arrive(this.gameObject, target, maxAcceleration, maxSpeed, targetRadius, slowRadius, timeToTarget);
                break;
            case Movement.Align:
                sb = new Align(this.gameObject, target, maxAcceleration, maxSpeed, targetRadius, slowRadius, timeToTarget);
                break;
            case Movement.Pursue:
                sb = new Pursue(this.gameObject, target, maxAcceleration);
                break;
            case Movement.Pursue2:
                sb = new Pursue2(this.gameObject, target, maxAcceleration, maxSpeed, targetRadius, slowRadius, timeToTarget);
                break;
            case Movement.Face:
                sb = new Face(this.gameObject, target, maxAcceleration, maxSpeed, targetRadius, slowRadius, timeToTarget);
                break;
            case Movement.Look:
                sb = new LookWhereYoureGoing(this.gameObject, target, maxAcceleration, maxSpeed, targetRadius, slowRadius, timeToTarget);
                break;
        }

        // Get the steering output of the selected behavior
        SteeringOutput steering = sb.GetSteering();

        // Update position and orientation
        transform.position += velocity * Time.fixedDeltaTime;
        transform.eulerAngles += new Vector3(0, 0, rotation * Time.fixedDeltaTime);

        // Update velocity and rotation
        velocity += steering.linear * Time.fixedDeltaTime;
        transform.eulerAngles += new Vector3(0, 0, steering.angular * Time.fixedDeltaTime);

        // If speed exceeds the maximum set it to the max
        if (velocity.magnitude > maxSpeed)
        {
            velocity.Normalize();
            velocity *= maxSpeed;
        } // If speed is less than minimum that stop
        else if (velocity.magnitude < minSpeed)
        {
            velocity *= 0;
        }
    }

    // Get velocity of character
    public Vector3 GetVelocity()
    {
        return velocity;
    }

    // Get velocity of character
    public float GetRotation()
    {
        return rotation;
    }
}
