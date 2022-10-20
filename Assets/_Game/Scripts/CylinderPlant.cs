using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aezakmi.Tweens;

public class CylinderPlant : PlantController
{

    private float startingScale, goalScale = 0.3f; //special case

    private float curScale = -1f; //special case

    public TweenBase tweenForPlanting = null; //special case

    public GameObject plantModel = null; //special case

    public Canvas progressCanvas = null; //special case

    public ProgressCircle progressCircle = null; //special case




    protected override void GrowingEffect(float progress)
    {
        Debug.Log(progress);
        curScale = startingScale + (goalScale - startingScale) * progress;
        plantModel.transform.localScale = new Vector3(curScale, curScale, curScale);

        progressCircle.SetProgress(progress);
    }

    public override void Plant()
    {
        //effect of planting
        float startingScale = plantModel.transform.localScale.x * 0.75f;
        plantModel.transform.localScale = new Vector3(plantModel.transform.localScale.x * 0.75f, plantModel.transform.localScale.y * 0.75f, plantModel.transform.localScale.z * 0.75f);

        tweenForPlanting.PlayTween();
        tweenForPlanting.AddDelegateOnComplete(Grow);

        progressCanvas.enabled = true;

    }

    public override void GrowInitThings()
    {
        startingScale = plantModel.transform.localScale.x;
        tweenForPlanting.RemoveDelegateOnComplete(Grow);
    }
}
