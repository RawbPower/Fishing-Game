using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Override the Align.target member target

public class Face : Align
{
    public Face(GameObject character, GameObject target, float maxAcceleration, float maxSpeed, float targetRadius, float slowRadius, float timeToTarget)
        : base(character, target, maxAcceleration, maxSpeed, targetRadius, slowRadius, timeToTarget) { }

    public override SteeringOutput GetSteering()
    {
        // 1. Calculate the target to delegate to align

        // Work out the direction to target
        Vector3 direction = target.transform.position - character.transform.position;

        // Check for a zero direction, and make no change if so
        // Not sure about this
        if (direction.magnitude == 0)
        {
            SteeringOutput steering = new SteeringOutput();
            steering.linear = Vector3.zero;
            steering.angular = 0f;
            return steering;
        }

        // Put the target together
        base.target = target;
        float original = base.target.transform.eulerAngles.z;
        base.target.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(-direction.x, direction.y)*(180/Mathf.PI));

        // 2. Delegate to align
        SteeringOutput faceSteer = base.GetSteering();
        base.target.transform.eulerAngles = new Vector3(0, 0, original);
        return faceSteer;

    }
}
