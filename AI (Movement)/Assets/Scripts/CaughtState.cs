using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaughtState : FishState
{
    public override FishState Update(FishController fish)
    {
        fish.transform.position = fish.target.transform.position;
        return null;
    }


    public override string ToString()
    {
        return "Caught State";
    }
}
