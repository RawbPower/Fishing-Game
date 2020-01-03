using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingState : PlayerState

{
    private Vector3 distance;

    public override PlayerState HandleInput(Fisherman player, Input input)
    {
        return null;
    }

    public override PlayerState Update(Fisherman player)
    {
        if (Input.GetButton("Action"))
        {
            distance = (player.transform.position - player.lure.transform.position);
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

    public override void Enter(Fisherman player)
    {
        player.lure.GetComponent<AIMovement>().maxSpeed = 3;
    }

    public override void Exit(Fisherman player)
    {
    }

    public override string ToString()
    {
        return "Waiting State";
    }
}
