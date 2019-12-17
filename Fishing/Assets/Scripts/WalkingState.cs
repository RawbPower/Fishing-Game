using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : PlayerState
{
    public override PlayerState HandleInput(FishermanController player, Input input)
    {

        return null;
    }

    public override PlayerState Update(FishermanController player)
    {
        if (Input.GetKey(KeyCode.X))
        {
            player.CastLure();
            return new WaitingState();
        }

        return null;
    }

    public override string ToString()
    {
        return "Walking State";
    }

    public override void Enter(FishermanController player)
    {
        player.RemoveLure();
    }

    public override void Exit(FishermanController player)
    {
    }
}