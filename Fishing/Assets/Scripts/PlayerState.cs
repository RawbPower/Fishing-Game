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
    public abstract void HandleInput(GameObject player, Input input);

    public abstract void Update(GameObject player);
}

