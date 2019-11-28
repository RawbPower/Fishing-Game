using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehavior
{
    public Seek(GameObject character, GameObject target, float maxAcceleration) : base(character, target, maxAcceleration) { }

    public override SteeringOutput GetSteering()
    {
        SteeringOutput steering = new SteeringOutput();

        steering.linear = target.transform.position - character.transform.position;

        steering.linear.Normalize();
        steering.linear = steering.linear * maxAcceleration;

        steering.angular = 0;

        return steering;

    }
}
