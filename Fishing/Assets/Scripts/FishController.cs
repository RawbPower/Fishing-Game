using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour, IFollowable
{
    // Public Variables

    // Max speed for character
    public float maxSpeed = 4;
    // Min speed. After this the character might as well have stopped. Because we only decrease acceleration it will keep decreasing 
    // smaller and smaller so we have to set it to zero at some point
    public double minSpeed = 0.2 * (1.0 / 60.0);

    public float maxAcceleration = 1;                   // Max linear acceleration for character
    public float rotation = 0;                          // Rotation of character
    public Vector3 velocity = Vector3.zero;             // Velocity of character           

    public float timeToTarget = 0.1f;                   // Causes the movement to slow down as it gets close so that it does overshoot
    public GameObject target;                           // Game Object for target

    public float targetRadius = 0.001f;                 // Radius at which the character is close enough to the target 
    public float slowRadius = 3.0f;                     // Radius at which the character will start to slow down

    [SerializeField]
    private FishState state;

    private void Awake()
    {
        state = new FreeState();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(state);
        FishState returnState = state.Update(this);
        if (returnState != null)
        {
            state = returnState;

            state.Enter(this);
        }
    }

    // Get velocity of character
    public Vector3 GetVelocity()
    {
        return velocity;
    }

    // Get velocity of character
    public float GetRotation()
    {
        return rotation;
    }
}
