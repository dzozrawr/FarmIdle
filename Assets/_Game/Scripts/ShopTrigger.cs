using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    private PlayerController player = null;

    private bool wasCCHit = false;
    private void Start()
    {
        player = GameController.Instance.playerController;
    }
/*     private void OnCollisionEnter(Collision other)
    {
        Debug.Log("OnCollisionEnter");
        if (other.gameObject == player.gameObject)
        {
            Debug.Log("player.SellBackpackContents();");
            player.SellBackpackContents();
        }
    } */

    private void OnTriggerEnter(Collider other)
    {
      //  Debug.Log("OnTriggerEnter");
        if ((player!=null)&&(other.gameObject == player.gameObject))    //player is null in the beginning, i guess because there is a triggerEnter event before Start()
        {
            player.SellBackpackContents(transform);
        }
    }

    /*     private void OnControllerColliderHit(ControllerColliderHit hit) {

            wasCCHit=true;
        } */



}
