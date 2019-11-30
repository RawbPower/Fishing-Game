/*
 * Flee
 * 
 * Steering algorithm to flee from a target game object.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : SteeringBehavior
{
    public Flee(GameObject character, GameObject target, float maxAcceleration) : base(character, target, maxAcceleration) { }

    // Return the desired steering output
    public override SteeringOutput GetSteering()
    {
        // Create the structure to hold our output
        SteeringOutput steering = new SteeringOutput();

        // Get the direction to the target
        steering.linear = - (target.transform.position - character.transform.position);

        // Give full acceleration along this direction
        steering.linear.Normalize();
        steering.linear = steering.linear * maxAcceleration;

        // Output the steering
        steering.angular = 0;
        return steering;

    }
}
