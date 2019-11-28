using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{

    [System.Serializable]
    public enum Movement { Seek, Flee, Arrive }

    // Public Variables
    public float maxSpeed = 4;
    public float maxAcceleration = 1;
    public float rotation = 0;

    private Vector3 velocity = Vector3.zero;
    private SteeringBehavior sb;

    public float timeToTarget = 0.25f;
    public GameObject target;
    public Movement movement;


    // Update is called once per frame
    void FixedUpdate()
    {
        switch (movement)
        {
            case Movement.Seek:
                sb = new Seek(this.gameObject, target, maxAcceleration);
                break;
            case Movement.Flee:
                sb = new Seek(this.gameObject, target, maxAcceleration);
                break;
            case Movement.Arrive:
                sb = new Seek(this.gameObject, target, maxAcceleration);
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
        }
    }
}
