using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellScript : MonoBehaviour
{
    private PlayerController player = null;

    private void Start()
    {
        player = GameController.Instance.playerController;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            if(!player.HasBucketOfWater){
                player.HasBucketOfWater=true;
                Debug.Log("Bucket filled!");
                //trigger animation, set bucket enabled and whatnot
            }
        }
    }
}
