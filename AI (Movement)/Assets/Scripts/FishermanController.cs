using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishermanController : PlayerController
{

    public GameObject lure; 

    private Vector3 distance;
    private PlayerState state;

    private void Start()
    {
        state = new WaitingState();
        lure.GetComponent<AIMovement>().movement = AIMovement.Movement.Face;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(state.ToString());
        state.Update(this.gameObject);
    }
}
