/*
 * Arrive
 * 
 * Steering algorithm to follow a target game object and then slow down as it approaches.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : SteeringBehavior
{
    private float maxSpeed;         // Max speed of character
    private float targetRadius;     // Radius at which character is close enough to the target
    private float slowRadius;       // Radius at which the character begins to slow down
    private float timeToTarget;     // Causes the movement to slow down as it gets close so that it does overshoot

    // Add new variable to the constructor
    public Stop(GameObject character, GameObject target, float maxAcceleration, float timeToTarget)
        : base(character, target, maxAcceleration)
    {
        this.timeToTarget = timeToTarget;
    }

    public override SteeringOutput GetSteering()
    {
        // Create the structure to hold our output
        SteeringOutput steering = new SteeringOutput();

        // Get direction to the target
        Vector3 direction = target.transform.position - character.transform.position;
        float distance = direction.magnitude;

        // Velocity that we want to have
        Vector3 targetVelocity;

        // The target velocity combines speed and direction
        targetVelocity = direction;
        targetVelocity.Normalize();
        targetVelocity *= maxSpeed;

        // Get characters current speed
        AIMovement aiMovement = character.GetComponent<AIMovement>();
        Vector3 characterVelocity = aiMovement.GetVelocity();

        // Acceleration tries to get to the target velocity
        steering.linear = targetVelocity - characterVelocity;
        steering.linear /= timeToTarget;

        // Check if the acceleration is too fast
        if (steering.linear.magnitude > maxAcceleration)
        {
            steering.linear.Normalize();
            steering.linear = steering.linear * maxAcceleration;
        }

        // Output the steering
        steering.angular = 0;
        return steering;

    }
}

