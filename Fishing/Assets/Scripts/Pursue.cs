using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursue : Seek
{
    public Pursue(GameObject character, GameObject target, float maxAcceleration) : base(character, target, maxAcceleration) { }

    // Holds maximum prediciton time
    public float maxPrediction = 1000;

    /* OVERRIDES the target data in seek (in others words
     * this class has two bits of data called target:
     * Seek.target is the superclass target which
     * will be automimatically calculated and shouldn't
     * be set, and Pursue.target is the target we're
     * pursuing).
     */

    public override SteeringOutput GetSteering()
    {
        // 1. Calculate the target to delegate seek

        // Work out the distance to the target
        Vector3 direction = target.transform.position - character.transform.position;
        float distance = direction.magnitude;

        // Work out our current speed
        float speed = character.GetComponent<AIMovement>().GetVelocity().magnitude;
        float prediction;

        //Debug.Log("speed: " + speed);
        //Debug.Log(distance / maxPrediction);

        // Check if speed is too small to give a reasonable prediction time
        if (speed <= distance/maxPrediction)
        {
            Debug.Log("Ding");
            prediction = maxPrediction;
        } else
        {
            prediction = distance / speed;
        }

        // Put the target together
        base.target = target;
        base.target.transform.position += target.GetComponent<PlayerController>().GetVelocity() * prediction;
        SteeringOutput pursueSteer = base.GetSteering();
        base.target.transform.position -= target.GetComponent<PlayerController>().GetVelocity() * prediction;

        return pursueSteer;
    }
}
