using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishermanController : PlayerController
{

    public GameObject lure;
    public float catchRadius = 1.2f;

    private Vector3 distance;
    private PlayerState state;
    public FishController fishOnHook;

    private void Start()
    {
        state = new WaitingState();
        lure.GetComponent<AIMovement>().movement = AIMovement.Movement.Face;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //base.FixedUpdate();
        //Debug.Log(state.ToString());
        PlayerState returnState = state.Update(this);
        if (returnState != null)
        {
            state = returnState;

            state.Enter(this);
        }
    }

    public PlayerState GetState()
    {
        return this.state;
    }

    public void SetState(PlayerState state)
    {
        this.state = state;
    }

    public void CatchFish()
    {
        if (fishOnHook != null)
        {
            Debug.Log("CAUGHT!!!");
            FishController caughtFish = fishOnHook;
            fishOnHook = null;
            Destroy(caughtFish.gameObject);
        }
    }
}
