/*
 * Arrive
 * 
 * Steering algorithm to follow a target game object and then slow down as it approaches.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : SteeringBehavior
{
    private float maxSpeed;         // Max speed of character
    private float targetRadius;     // Radius at which character is close enough to the target
    private float slowRadius;       // Radius at which the character begins to slow down
    private float timeToTarget;     // Causes the movement to slow down as it gets close so that it does overshoot

    // Add new variable to the constructor
    public Arrive(GameObject character, GameObject target, float maxAcceleration, float maxSpeed, float targetRadius, float slowRadius, float timeToTarget) 
        : base(character, target, maxAcceleration) {

        this.maxSpeed = maxSpeed;
        this.targetRadius = targetRadius;
        this.slowRadius = slowRadius;
        this.timeToTarget = timeToTarget;

    }

    public override SteeringOutput GetSteering()
    {
        // Create the structure to hold our output
        SteeringOutput steering = new SteeringOutput();

        // Get direction to the target
        Vector3 direction = target.transform.position - character.transform.position;
        float distance = direction.magnitude;

        // Check if we are there, return no steering
        if (distance < targetRadius)
        {
            Debug.Log("Piss");
            steering.linear = Vector3.zero;
            steering.angular = 0f;
            return steering;
        }

        // This is the speed that we want to have
        float targetSpeed;

        // If we are outside the slow radius, the go to max speed
        if (distance > slowRadius)
        {
            targetSpeed = maxSpeed;
        }   // Otherwise calculate scaled speed
        else
        {
            //Debug.Log("Pingo " + character.name);
            targetSpeed = maxSpeed * (distance / slowRadius);
        }

        // Velocity that we want to have
        Vector3 targetVelocity;

        // The target velocity combines speed and direction
        targetVelocity = direction;
        targetVelocity.Normalize();
        targetVelocity *= targetSpeed;

        Vector3 characterVelocity = Vector3.zero;

        // Get characters current speed
        if (character.GetComponent<AIMovement>() != null)
        {
            AIMovement aiMovement = character.GetComponent<AIMovement>();
            characterVelocity = aiMovement.GetVelocity();
        } else if (character.GetComponent<FishController>() != null)
        {
            FishController fishController = character.GetComponent<FishController>();
            characterVelocity = fishController.GetVelocity();
        } else
        {
            throw new System.Exception("The is no component of the game object " + character.name + " which has a velocity parameter (ie. AIMovement or FishController)");
        }

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
