/*
 * Steering Behavior
 * 
 * Super class for the different types of steering behaviors.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class SteeringBehavior
{
    protected GameObject character;             // Variable to hold character game object
    protected GameObject target;                // Variable to hold target game object
    protected float maxAcceleration;            // Maximum linear acceleration of character

    // Constructor of the class to set initial variable from inspector input fields
    protected SteeringBehavior(GameObject character, GameObject target, float maxAcceleration)
    {
        this.character = character;
        this.target = target;
        this.maxAcceleration = maxAcceleration;
    }

    // Abstract file for steering movement
    public abstract SteeringOutput GetSteering();
}
