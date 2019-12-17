using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishermanController : PlayerController
{
    public GameObject lureType;
    public GameObject lure;
    private bool lureCast;
    public float catchRadius = 3.0f;

    private Vector3 distance;
    private PlayerState state;
    public FishController fishOnHook;

    private void Start()
    {
        state = new WalkingState();
        //lure.GetComponent<AIMovement>().movement = AIMovement.Movement.Face;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //base.FixedUpdate();
        //Debug.Log(state);
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

    public void RemoveLure()
    {
        GameObject removedLure = lure;
        lure = null;
        Destroy(removedLure.gameObject);
        lureCast = false;
    }

    public void CastLure()
    {
        Debug.Log("DJOSIJ");
        if (lureCast == false)
        {
            lureType.GetComponent<AIMovement>().target = this.gameObject;
            Instantiate(lureType);
            lure = GameObject.FindGameObjectWithTag("Lure");
            GameObject[] fishes;
            fishes = GameObject.FindGameObjectsWithTag("Fish");

            foreach (GameObject f in fishes) {
                f.GetComponent<FishController>().target = lure;
            }
            lureCast = true;
        }
    }
}
