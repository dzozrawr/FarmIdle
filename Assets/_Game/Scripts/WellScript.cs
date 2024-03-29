using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WellScript : MonoBehaviour
{
    public Aezakmi.Tweens.Scale scaleEffectTween = null;
    public GameObject wellModel = null;
    private PlayerController player = null;

    private void Start()
    {
        player = GameController.Instance.playerController;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            if (!player.HasBucketOfWater)
            {
                player.SetBucketExistence(true);

                //GameObject plantModel = models[0];
                wellModel.transform.DOPunchScale(Vector3.one*0.1f,0.2f);    

                scaleEffectTween.PlayTween();
                SoundManager.Instance.PlaySound("bucketFillSound");

                player.guidingIndicator.SetTargetAndEnable(GameController.Instance.GetClosestPlantTriggerCircle(player.transform));
                // tweenForPlanting.AddDelegateOnComplete(Grow);
                //                Debug.Log("Bucket filled!");
                //trigger animation, set bucket enabled and whatnot
            }
        }
    }
}
