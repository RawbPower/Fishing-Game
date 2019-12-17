using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookedState : PlayerState
{

    private Vector3 distance;

    public override PlayerState HandleInput(FishermanController player, Input input)
    {
        return null;
    }

    public override PlayerState Update(FishermanController player)
    {

        distance = (player.transform.position - player.lure.transform.position);
        Debug.Log(distance.magnitude);
        if (distance.magnitude < player.catchRadius)
        {
            player.CatchFish();
            return new WalkingState();
        }

        if (Input.GetButton("Action"))
        {
            player.lure.GetComponent<AIMovement>().target = player.gameObject;
            player.lure.GetComponent<AIMovement>().movement = AIMovement.Movement.Arrive;
        }
        else if (player.lure.GetComponent<AIMovement>().velocity.magnitude > player.lure.GetComponent<AIMovement>().minSpeed)
        {
            player.lure.GetComponent<AIMovement>().movement = AIMovement.Movement.Stop;
        }
        else
        {
            player.lure.GetComponent<AIMovement>().velocity = Vector3.zero;
            player.lure.GetComponent<AIMovement>().target = player.gameObject;
            player.lure.GetComponent<AIMovement>().movement = AIMovement.Movement.Face;
        }

        return null;
    }

    public override string ToString()
    {
        return "Hooked State";
    }

    public override void Enter(FishermanController player)
    {
        player.lure.GetComponent<AIMovement>().maxSpeed = 1;
    }

    public override void Exit(FishermanController player)
    {
    }
}
