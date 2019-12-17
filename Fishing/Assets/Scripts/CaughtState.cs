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

    public override void Enter(FishController fish)
    {
        fish.target.GetComponent<AIMovement>().target.GetComponent<FishermanController>().fishOnHook = fish;
        fish.target.GetComponent<AIMovement>().target.GetComponent<FishermanController>().SetState(new HookedState());
        fish.target.GetComponent<AIMovement>().target.GetComponent<FishermanController>().GetState().Enter(fish.target.GetComponent<AIMovement>().target.GetComponent<FishermanController>());
    }

    public override void Exit(FishController fish)
    {
    }
}
