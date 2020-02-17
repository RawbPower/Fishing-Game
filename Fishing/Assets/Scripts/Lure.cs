/*
 * Lure Controller
 * 
 * Unity script for control basic movement of lure
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lure : MonoBehaviour
{

    public Fisherman player;
    public float catchRadius;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pond" && collision.GetType() == typeof(EdgeCollider2D))
        {
            Debug.Log("Reel it boy!");
            player.CatchFish();
            player.SetState(new WalkingState());
        }
    }

    public void SetPlayer(Fisherman player)
    {
        this.player = player;
        this.catchRadius = player.catchRadius;
    }
}
