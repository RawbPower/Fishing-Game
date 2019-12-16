using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookedState : PlayerState
{
    public override void HandleInput(GameObject player, Input input)
    {

    }

    public override void Update(GameObject player)
    {
    }

    public override string ToString()
    {
        return "Hooked State";
    }
}
