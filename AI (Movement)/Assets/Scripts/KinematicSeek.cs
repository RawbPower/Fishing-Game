using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicSeek : MonoBehaviour
{

    // Public Variables
    public float maxSpeed = 4;

    public float radius = 1;
    public float timeToTarget = 0.25f;
    public GameObject target;

    void FixedUpdate()
    {

        KinematicSteeringOutput character = new KinematicSteeringOutput();

        character = GetSteering(target);

        transform.position = transform.position + character.vel * Time.fixedDeltaTime;

        transform.eulerAngles = new Vector3(0, 0, character.rot);

        character.rot = 0;

    }

    float GetNewOrientation(float currentOrientation, Vector3 velocity)
    {

        if (velocity.magnitude > 0)
        {
            return Mathf.Atan2(-velocity.x, velocity.y) * (180 / (Mathf.PI));
        }
        else
        {
            return currentOrientation;
        }

    }

    KinematicSteeringOutput GetSteering(GameObject target)
    {
        KinematicSteeringOutput steering = new KinematicSteeringOutput();

        steering.vel = target.transform.position - transform.position;

        if (steering.vel.magnitude < radius)
        {
            KinematicSteeringOutput noVel = new KinematicSteeringOutput();
            noVel.vel = Vector3.zero;
            noVel.rot = steering.rot;
            return noVel;
        }

        steering.vel = steering.vel / timeToTarget;

        if (steering.vel.magnitude > maxSpeed)
        {
            steering.vel.Normalize();
            steering.vel = steering.vel * maxSpeed;
        }

        steering.rot = GetNewOrientation(transform.rotation.eulerAngles.z, steering.vel);

        return steering;
    }

    public struct KinematicSteeringOutput {
        public Vector3 vel;
        public float rot;
    }

}
