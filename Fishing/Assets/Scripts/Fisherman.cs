using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fisherman : MonoBehaviour
{
    public GameObject lureType;
    public GameObject lure;
    public GameObject marker;
    private bool lureCast;
    public float catchRadius = 3.0f;

    public float castAngle;
    public float castSpeed;
    public float height = 3;

    private Vector3 distance;

    private float g = 1.0f;

    [SerializeField]
    public PlayerState state;

    public FishController fishOnHook;

    private void Start()
    {
        state = new WalkingState();
        //lure.GetComponent<AIMovement>().movement = AIMovement.Movement.Face;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //base.FixedUpdate();
        //Debug.Log(state);
        PlayerState returnState = state.Update(this);
        if (returnState != null)
        {
            state = returnState;

            state.Enter(this);
        }
    }

    public PlayerState GetState()
    {
        return this.state;
    }

    public void SetState(PlayerState state)
    {
        this.state = state;
        state.Enter(this);
    }

    public void CatchFish()
    {
        if (fishOnHook != null)
        {
            Debug.Log("CAUGHT!!!");
            FishController caughtFish = fishOnHook;
            fishOnHook = null;
            Destroy(caughtFish.gameObject);
        }
    }

    public void RemoveLure()
    {
        GameObject removedLure = lure;
        lure = null;
        Destroy(removedLure.gameObject);
        lureCast = false;
    }

    public bool CastLure()
    {
        if (lureCast == false)
        {
            float range = GetRange(castSpeed, castAngle, height);
            Debug.Log(range);
            float dir = gameObject.GetComponent<PlayerController>().rotation;
            Vector2 pos = new Vector2(transform.position.x - range * Mathf.Sin(dir * Mathf.Deg2Rad), transform.position.y + range * Mathf.Cos(dir * Mathf.Deg2Rad));
            if (Physics2D.OverlapCircle(pos, 0.5f).gameObject.name == "Pond")
            {
                lureType.GetComponent<AIMovement>().target = this.gameObject;
                Instantiate(lureType);
                lure = GameObject.FindGameObjectWithTag("Lure");
                lure.GetComponent<Lure>().SetPlayer(this);
                lure.transform.position = new Vector3(pos.x, pos.y, 0.0f);
                GameObject[] fishes;
                fishes = GameObject.FindGameObjectsWithTag("Fish");

                foreach (GameObject f in fishes)
                {
                    f.GetComponent<FishController>().target = lure;
                }
                lureCast = true;
                return true;
            } else
            {
                Debug.Log("Not in the Pond");
                return false;
            }
        } else
        {
            return false;
        }
    }

    public float GetRange(float speed, float angle, float h)
    {
        //return (speed*speed*Mathf.Sin(2*angle*(Mathf.PI/180))/g);
        float angleRad = angle * Mathf.Deg2Rad;
        float xSpeed = speed * Mathf.Cos(angleRad);
        float time = ((speed * Mathf.Sin(angleRad)) / g) + Mathf.Sqrt(((speed*speed*Mathf.Sin(angleRad) * Mathf.Sin(angleRad)) / (g*g)) + ((2*h) / (g)));
        return xSpeed * time;
    }
}
