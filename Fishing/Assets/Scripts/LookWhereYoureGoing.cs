using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookWhereYoureGoing : Align
{
    public LookWhereYoureGoing(GameObject character, GameObject target, float maxAcceleration, float maxSpeed, float targetRadius, float slowRadius, float timeToTarget)
        : base(character, target, maxAcceleration, maxSpeed, targetRadius, slowRadius, timeToTarget) { }

    public override SteeringOutput GetSteering()
    {
        // 1. Calculate the target to delegate to align

        // Check for a zero direction, and make no change if so
        // Not sure about this
        if (character.GetComponent<AIMovement>().GetVelocity().magnitude == 0)
        {
            SteeringOutput steering = new SteeringOutput();
            steering.linear = Vector3.zero;
            steering.angular = 0f;
            return steering;
        }

        // Put the target together
        Debug.Log("Yo");
        float original = base.target.transform.eulerAngles.z;
        base.target.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(-character.GetComponent<AIMovement>().GetVelocity().x, character.GetComponent<AIMovement>().GetVelocity().y) * (180 / Mathf.PI));

        // 2. Delegate to align
        SteeringOutput lookSteer = base.GetSteering();
        base.target.transform.eulerAngles = new Vector3(0, 0, original);
        return lookSteer;

    }
}
