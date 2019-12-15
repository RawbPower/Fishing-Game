using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingState : PlayerState

{
    private Vector3 distance;

    public override void HandleInput(GameObject player, Input input)
    {
    }

    public override void Update(GameObject player)
    {
        if (Input.GetButton("Action"))
        {
            distance = (player.transform.position - player.GetComponent<FishermanController>().lure.transform.position);
            player.GetComponent<FishermanController>().lure.GetComponent<AIMovement>().target = player.gameObject;
            player.GetComponent<FishermanController>().lure.GetComponent<AIMovement>().movement = AIMovement.Movement.Arrive;
        }
        else if (player.GetComponent<FishermanController>().lure.GetComponent<AIMovement>().velocity.magnitude > player.GetComponent<FishermanController>().lure.GetComponent<AIMovement>().minSpeed)
        {
            player.GetComponent<FishermanController>().lure.GetComponent<AIMovement>().movement = AIMovement.Movement.Stop;
        }
        else
        {
            player.GetComponent<FishermanController>().lure.GetComponent<AIMovement>().velocity = Vector3.zero;
            player.GetComponent<FishermanController>().lure.GetComponent<AIMovement>().target = player.gameObject;
            player.GetComponent<FishermanController>().lure.GetComponent<AIMovement>().movement = AIMovement.Movement.Face;
        }
    }

    public override string ToString()
    {
        return "Waiting State";
    }
}
