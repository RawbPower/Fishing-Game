using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastAngleUI : MonoBehaviour
{

    public float arcSpeed = 45;
    public Fisherman fisherman;

    private bool forwardArc = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 point = new Vector3(-15.0f, -6.0f, 0.0f);
        Vector3 axis = new Vector3(0, 0, 1);
        //Debug.Log(transform.eulerAngles.z);
        if (forwardArc && transform.eulerAngles.z > 91.0f && transform.eulerAngles.z < 270.0f)
        {
            forwardArc = false;
        } else if (!forwardArc && transform.eulerAngles.z > 90.0f && transform.eulerAngles.z < 269.0f)
        {
            forwardArc = true;
        }
        float rot = forwardArc ? -Time.deltaTime * arcSpeed : Time.deltaTime * arcSpeed;
        transform.RotateAround(point, axis, rot);
        fisherman.castAngle = transform.eulerAngles.z;
    }
}
