/*
 * Steering Output
 * 
 * Struct to hold main parameters for steering
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SteeringOutput
{
    public Vector3 linear;       // Linear acceleration
    public float angular;        // Angular acceleration around axis perpindicular to the plane
}
