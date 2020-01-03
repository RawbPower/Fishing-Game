/*
 * Player State
 * 
 * Interface defining states for the state machine of the player
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
abstract public class PlayerState
{
    public abstract PlayerState HandleInput(Fisherman player, Input input);

    public abstract PlayerState Update(Fisherman player);

    public abstract void Enter(Fisherman player);

    public abstract void Exit(Fisherman player);
}

