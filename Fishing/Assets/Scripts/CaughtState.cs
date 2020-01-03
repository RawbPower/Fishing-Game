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
        Fisherman fisherman = fish.target.GetComponent<AIMovement>().target.GetComponent<Fisherman>();
        fisherman.fishOnHook = fish;
        fisherman.SetState(new HookedState());
        fisherman.GetState().Enter(fish.target.GetComponent<AIMovement>().target.GetComponent<Fisherman>());
        Debug.Log("Caught!");

    }

    public override void Exit(FishController fish)
    {
    }
}
