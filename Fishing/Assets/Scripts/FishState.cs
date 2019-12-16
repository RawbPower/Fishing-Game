/*
 * Player State
 * 
 * Interface defining states for the state machine of the player
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class FishState
{

    public abstract FishState Update(FishController fish);
}

