using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : SteeringBehavior
{

    private float maxSpeed;
    private float targetRadius;
    private float slowRadius;
    private float timeToTarget;

    public Arrive(GameObject character, GameObject target, float maxAcceleration, float maxSpeed, float targetRadius, float slowRadius, float timeToTarget) 
        : base(character, target, maxAcceleration) {

        this.maxSpeed = maxSpeed;
        this.targetRadius = targetRadius;
        this.slowRadius = slowRadius;
        this.timeToTarget = timeToTarget;

    }

    public override SteeringOutput GetSteering()
    {
        SteeringOutput steering = new SteeringOutput();

        Vector3 direction = target.transform.position - character.transform.position;
        float distance = direction.magnitude;

        if (distance < targetRadius)
        {
            Debug.Log("Piss");
            steering.linear = Vector3.zero;
            steering.angular = 0f;
            return steering;
        }

        float targetSpeed;

        if (distance > slowRadius)
        {
            targetSpeed = maxSpeed;
        } else
        {
            targetSpeed = maxSpeed * (distance / slowRadius);
        }

        Vector3 targetVelocity;

        targetVelocity = direction;
        targetVelocity.Normalize();
        targetVelocity *= targetSpeed;

        AIMovement aiMovement = character.GetComponent<AIMovement>();

        Vector3 characterVelocity = aiMovement.GetVelocity();

        steering.linear = targetVelocity - characterVelocity;
        steering.linear /= timeToTarget;

        if (steering.linear.magnitude > maxAcceleration)
        {
            steering.linear.Normalize();
            steering.linear = steering.linear * maxAcceleration;
        }

        steering.angular = 0;

        return steering;

    }
}
