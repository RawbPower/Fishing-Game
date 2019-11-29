using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AIMovement : MonoBehaviour
{

    [System.Serializable]
    public enum Movement { Seek, Flee, Arrive }

    // Public Variables
    public float maxSpeed = 4;
    public float maxAcceleration = 1;
    public float rotation = 0;

    public Vector3 velocity = Vector3.zero;
    private SteeringBehavior sb;
    public double minSpeed;

    public float timeToTarget = 0.1f;
    public GameObject target;
    public Movement movement;

    public float targetRadius = 0.8f;
    public float slowRadius = 2.0f;

    private void Awake()
    {
        minSpeed = 0.2 * Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        switch (movement)
        {
            case Movement.Seek:
                sb = new Seek(this.gameObject, target, maxAcceleration);
                break;
            case Movement.Flee:
                sb = new Flee(this.gameObject, target, maxAcceleration);
                break;
            case Movement.Arrive:
                sb = new Arrive(this.gameObject, target, maxAcceleration, maxSpeed, targetRadius, slowRadius, timeToTarget);
                break;
        }

        SteeringOutput steering = sb.GetSteering();

        transform.position += velocity * Time.fixedDeltaTime;
        transform.eulerAngles += new Vector3(0, 0, rotation * Time.fixedDeltaTime);

        velocity += steering.linear * Time.fixedDeltaTime;
        transform.eulerAngles += new Vector3(0, 0, steering.angular * Time.fixedDeltaTime);

        if (velocity.magnitude > maxSpeed)
        {
            velocity.Normalize();
            velocity *= maxSpeed;
        } else if (velocity.magnitude < minSpeed)
        {
            velocity *= 0;
        }
    }

    public Vector3 GetVelocity()
    {
        return velocity;
    }
}
