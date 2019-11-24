using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicWander : MonoBehaviour
{

    // Public Variables
    public float maxSpeed = 4;
    public float maxRotation = 10;

    void FixedUpdate()
    {

        KinematicSteeringOutput character = new KinematicSteeringOutput();

        character = GetSteering();

        transform.position = transform.position + character.vel * Time.fixedDeltaTime;

        transform.eulerAngles = new Vector3(0, 0, character.rot);

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

    KinematicSteeringOutput GetSteering()
    {
        KinematicSteeringOutput steering = new KinematicSteeringOutput();

        steering.vel = new Vector3(-Mathf.Sin(transform.eulerAngles.z*((Mathf.PI)/180))*maxSpeed, Mathf.Cos(transform.eulerAngles.z*((Mathf.PI)/180))*maxSpeed, 0);

        steering.rot = steering.rot + Random.Range(-1.0f, 1.0f)*maxRotation;

        return steering;
    }

    float RandomBinomial()
    {
        float rando = Random.Range(0.0f, 1.0f);
        return Random.Range(1.0f, 2.0f) - rando;
    }

    public struct KinematicSteeringOutput
    {
        public Vector3 vel;
        public float rot;
    }

}