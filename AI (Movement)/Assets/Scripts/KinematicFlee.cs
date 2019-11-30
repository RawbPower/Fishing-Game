/*
 * Kinematic Flee
 * 
 * Unity script for a simple kinematic AI that flees from the target GameObject
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicFlee : MonoBehaviour
{

    // Public Variables
    public float maxSpeed = 4;              // Holds the maximum speed that the character can travel

    // GameObject of the target
    public GameObject target;

    void FixedUpdate()
    {
        // Creat a variable for the steering of the character
        KinematicSteeringOutput character = GetSteering(target);

        // Update position of character
        transform.position = transform.position + character.vel * Time.fixedDeltaTime;

        // Update orientation of character
        transform.eulerAngles = new Vector3(0, 0, character.rot);

        // Reset rotation
        character.rot = 0;

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

    // Calculate steering for a seeking AI
    KinematicSteeringOutput GetSteering(GameObject target)
    {
        // Create structure for output
        KinematicSteeringOutput steering = new KinematicSteeringOutput();

        // Get opposite direction to the target 
        steering.vel = transform.position - target.transform.position;

        // Set velocity to max speed
        steering.vel.Normalize();
        steering.vel = steering.vel * maxSpeed;

        // Face the direction that the character is moving
        steering.rot = GetNewOrientation(transform.rotation.eulerAngles.z, steering.vel);

        return steering;
    }

    // A struct to hold steering output
    public struct KinematicSteeringOutput
    {
        public Vector3 vel;
        public float rot;
    }

}