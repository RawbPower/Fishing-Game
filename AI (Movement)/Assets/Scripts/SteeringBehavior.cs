using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class SteeringBehavior : MonoBehaviour
{
    protected GameObject character;
    protected GameObject target;
    protected float maxAcceleration;

    protected SteeringBehavior(GameObject character, GameObject target, float maxAcceleration)
    {
        this.character = character;
        this.target = target;
        this.maxAcceleration = maxAcceleration;
    }

    public abstract SteeringOutput GetSteering();
}
