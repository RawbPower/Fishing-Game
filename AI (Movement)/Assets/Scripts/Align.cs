/*
 * Align
 * 
 * Steering algorithm to align with a target a target game object and then slow down as it approaches.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Align : SteeringBehavior
{
    private float maxSpeed;         // Max speed of character
    private float targetRadius;     // Radius at which character is close enough to the target
    private float slowRadius;       // Radius at which the character begins to slow down
    private float timeToTarget;     // Causes the movement to slow down as it gets close so that it does overshoot

    // Add new variable to the constructor
    public Align(GameObject character, GameObject target, float maxAcceleration, float maxSpeed, float targetRadius, float slowRadius, float timeToTarget)
        : base(character, target, maxAcceleration)
    {

        this.maxSpeed = maxSpeed;
        this.targetRadius = targetRadius;
        this.slowRadius = slowRadius;
        this.timeToTarget = timeToTarget;

    }

    public override SteeringOutput GetSteering()
    {
        // Create the structure to hold our output
        SteeringOutput steering = new SteeringOutput();

        // Get orientation to the target
        float rotation = target.transform.eulerAngles.z - character.transform.eulerAngles.z;

        // Map the result to the (-pi, pi) interval
        rotation = MapToRange(rotation);
        float rotationSize = Mathf.Abs(rotation);
        Debug.Log(rotation);

        // Check if we are there, return no steering
        if (rotationSize < targetRadius)
        {
            Debug.Log("Piss");
            steering.linear = Vector3.zero;
            steering.angular = 0f;
            return steering;
        }

        // This is the orientation that we want to have
        float targetRotation;

        // If we are outside the slow radius, the go to max speed
        if (rotationSize > slowRadius)
        {
            targetRotation = maxSpeed;
        }   // Otherwise calculate scaled orientation
        else
        {
            targetRotation = maxSpeed * rotationSize / slowRadius;
        }

        // The final target rotation combines speed (already in the variable) and direction
        targetRotation *= rotation/rotationSize;

        // Get characters current orientation
        AIMovement aiMovement = character.GetComponent<AIMovement>();
        float characterRotation = aiMovement.GetRotation();

        // Acceleration tries to get to the target orientation
        steering.angular = targetRotation - characterRotation;
        steering.angular /= timeToTarget;

        // Check if the acceleration is too fast
        float angularAcceleration = Mathf.Abs(steering.angular);
        if (angularAcceleration > maxAcceleration)
        {
            steering.angular /= angularAcceleration;
            steering.angular = steering.angular * maxAcceleration;
        }

        // Output the steering
        steering.linear = Vector3.zero;
        return steering;

    }

    // Makes sure orientation is within (-pi, pi)
    private float MapToRange(float rotation)
    {
        while (Mathf.Abs(rotation) > 180)
        {
            rotation += -Mathf.Sign(rotation)*(360);
        }

        return rotation;
    }
}
