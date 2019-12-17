/*
 * Player State
 * 
 * Interface defining states for the state machine of the player
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class PlayerState
{
    public abstract PlayerState HandleInput(FishermanController player, Input input);

    public abstract PlayerState Update(FishermanController player);

    public abstract void Enter(FishermanController player);

    public abstract void Exit(FishermanController player);
}

