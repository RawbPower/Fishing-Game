using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishermanController : PlayerController
{

    public GameObject lure;

    private Vector3 distance;

    private void Start()
    {
        lure.GetComponent<AIMovement>().movement = AIMovement.Movement.Face;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("space"))
        {
            distance = (transform.position - lure.transform.position);
            lure.GetComponent<AIMovement>().target = this.gameObject;
            lure.GetComponent<AIMovement>().movement = AIMovement.Movement.Arrive;
        } else if (lure.GetComponent<AIMovement>().velocity.magnitude > lure.GetComponent<AIMovement>().minSpeed)
        {
            lure.GetComponent<AIMovement>().movement = AIMovement.Movement.Stop;
        } else
        {
            lure.GetComponent<AIMovement>().velocity = Vector3.zero;
            lure.GetComponent<AIMovement>().target = this.gameObject;
            lure.GetComponent<AIMovement>().movement = AIMovement.Movement.Face;
        }
    }
}
