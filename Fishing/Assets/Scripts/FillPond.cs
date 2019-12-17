using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillPond : MonoBehaviour
{

    public GameObject fish;
    public GameObject lure;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Fish") == null)
        {
            CreateFish();
        }
    }

    void CreateFish()
    {
        fish.GetComponent<FishController>().target = null;
        Instantiate(fish);
    }
}
