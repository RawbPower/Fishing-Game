/*
 * Kinematic Wander
 * 
 * Unity script for a simple kinematic AI that wanders around
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicWander : MonoBehaviour
{

    // Public Variables
    public float maxSpeed = 4;                  // Holds the maximum speed that the character can travel
    public float maxRotation = 10;              // Holds the maximum rotation that the character can have

    void FixedUpdate()
    {
        // Creat a variable for the steering of the character
        KinematicSteeringOutput character = character = GetSteering();

        // Update position of character
        transform.position = transform.position + character.vel * Time.fixedDeltaTime;

        // Update orientation of character
        transform.eulerAngles = new Vector3(0, 0, character.rot);

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
    KinematicSteeringOutput GetSteering()
    {
        // Create structure for output
        KinematicSteeringOutput steering = new KinematicSteeringOutput();

        // Move in the direction of orientation
        steering.vel = new Vector3(-Mathf.Sin(transform.eulerAngles.z*((Mathf.PI)/180))*maxSpeed, Mathf.Cos(transform.eulerAngles.z*((Mathf.PI)/180))*maxSpeed, 0);

        // Get random orientation
        steering.rot = steering.rot + Random.Range(-1.0f, 1.0f)*maxRotation;

        return steering;
    }

    // Selected random number weighted in one direction
    float RandomBinomial()
    {
        float rando = Random.Range(0.0f, 1.0f);
        return Random.Range(1.0f, 2.0f) - rando;
    }

    // A struct to hold steering output
    public struct KinematicSteeringOutput
    {
        public Vector3 vel;
        public float rot;
    }

}