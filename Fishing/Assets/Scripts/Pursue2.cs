/* Note to self: Right now the code checks how much time it takes
 * to get to the targets current position and then calcualtes where 
 * the target would be after that about of time and goes in that 
 * direction. There has to be a better way to do this. If the target
 * get further away in that time than the predition will be short.
 * There's probably some intersecting line equation that would work
 * better.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursue2 : Arrive
{
    public Pursue2(GameObject character, GameObject target, float maxAcceleration, float maxSpeed, float targetRadius, float slowRadius, float timeToTarget)
        : base(character, target, maxAcceleration, maxSpeed, targetRadius, slowRadius, timeToTarget) {
    }

    // Holds maximum prediciton time
    public float maxPrediction = 1000;

    /* OVERRIDES the target data in seek (in others words
     * this class has two bits of data called target:
     * Arrive.target is the superclass target which
     * will be automimatically calculated and shouldn't
     * be set, and Pursue2.target is the target we're
     * pursuing).
     */

    public override SteeringOutput GetSteering()
    {
        // 1. Calculate the target to delegate seek

        // Work out the distance to the target
        Vector3 direction = target.transform.position - character.transform.position;
        float distance = direction.magnitude;

        // Work out our current speed
        float speed;
        speed = character.GetComponent<IFollowable>().GetVelocity().magnitude;
        /*if (character.GetComponent<AIMovement>() != null)
        {
            speed = character.GetComponent<AIMovement>().GetVelocity().magnitude;
        }
        else if (character.GetComponent<FishController>() != null)
        {
            speed = character.GetComponent<FishController>().GetVelocity().magnitude;
        }
        else
        {
            throw new System.Exception("The is no component of the game object " + character.name + " which has a velocity parameter (ie. AIMovement or FishController)");
        }*/
        float prediction;

        //Debug.Log("speed: " + speed);
        //Debug.Log(distance / maxPrediction);

        // Check if speed is too small to give a reasonable prediction time
        if (speed <= distance / maxPrediction)
        {
            prediction = maxPrediction;
        }
        else
        {
            prediction = distance / speed;
        }

        // Put the target together
        base.target = target;
        SteeringOutput pursueSteer;

        if (target.GetComponent<AIMovement>() != null)
        {
            base.target.transform.position += target.GetComponent<AIMovement>().GetVelocity() * prediction;
            pursueSteer = base.GetSteering();
            base.target.transform.position -= target.GetComponent<AIMovement>().GetVelocity() * prediction;
        }
        else if (target.GetComponent<PlayerController>() != null)
        {
            base.target.transform.position += target.GetComponent<PlayerController>().GetVelocity() * prediction;
            pursueSteer = base.GetSteering();
            base.target.transform.position -= target.GetComponent<PlayerController>().GetVelocity() * prediction;
        }
        else
        {
            throw new System.Exception("The is no component of the game object " + character.name + " which has a velocity parameter (ie. AIMovement or FishController)");
        }

        return pursueSteer;
    }
}
