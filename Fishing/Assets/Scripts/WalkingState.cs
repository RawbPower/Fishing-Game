﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : PlayerState
{
    public override PlayerState HandleInput(Fisherman player, Input input)
    {

        return null;
    }

    public override PlayerState Update(Fisherman player)
    {
        if (Input.GetKey(KeyCode.X))
        {
            bool cast = player.CastLure();
            if (cast)
            {
                return new WaitingState();
            }
            else
            {
                return null;
            }
        }

        return null;
    }

    public override string ToString()
    {
        return "Walking State";
    }

    public override void Enter(Fisherman player)
    {
        player.RemoveLure();
    }

    public override void Exit(Fisherman player)
    {
    }
}