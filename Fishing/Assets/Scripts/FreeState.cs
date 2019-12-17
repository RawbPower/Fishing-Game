using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeState : FishState
{

    private SteeringBehavior sb;                        // Variable for holding steering type

    public override FishState Update(FishController fish)
    {
        sb = new Pursue2(fish.gameObject, fish.target, fish.maxAcceleration, fish.maxSpeed, fish.targetRadius, fish.slowRadius, fish.timeToTarget);
        //sb = new Pursue(fish.gameObject, target, maxAcceleration);
        //Debug.Log(fish.name + " " + target.name + " " + maxAcceleration + " " + maxSpeed + " " + targetRadius + " " + slowRadius + " " + timeToTarget);

        // Get the steering output of the selected behavior
        SteeringOutput steering = sb.GetSteering();
        //Debug.Log(steering.linear + " Free");

        // Update position and orientation
        fish.transform.position += fish.velocity * Time.fixedDeltaTime;
        fish.transform.eulerAngles += new Vector3(0, 0, fish.rotation * Time.fixedDeltaTime);

        // Update velocity and rotation
        fish.velocity += steering.linear * Time.fixedDeltaTime;
        fish.transform.eulerAngles += new Vector3(0, 0, steering.angular * Time.fixedDeltaTime);

        // If speed exceeds the maximum set it to the max
        if (fish.velocity.magnitude > fish.maxSpeed)
        {
            fish.velocity.Normalize();
            fish.velocity *= fish.maxSpeed;
        } // If speed is less than minimum that stop
        else if (fish.velocity.magnitude < fish.minSpeed)
        {
            fish.velocity *= 0;
            return new CaughtState();
        }

        return null;
    }

    public override string ToString()
    {
        return "Free State";
    }

    public override void Enter(FishController fish)
    {
    }

    public override void Exit(FishController fish)
    {
    }
}
