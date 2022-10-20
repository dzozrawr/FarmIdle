using System.Collections;
using System.Collections.Generic;
using Aezakmi.Tweens;
using UnityEngine;

public abstract class PlantController : MonoBehaviour
{
    protected bool isGrowing = false;
    protected float growingMaxTime = 1f;
    protected float growingTime = 0f;
    protected float progress = 0f;

    public delegate void Notify();

    public event Notify PlantGrown;




    // Update is called once per frame
    protected void Update()
    {
        if (isGrowing)
        {
            progress = growingTime / growingMaxTime;

            //visible effect of the progress
            GrowingEffect(progress);    //could be scaling, could be animation, could be anything

            growingTime += Time.deltaTime;

            if (growingTime >= growingMaxTime)
            {
                progress = 1f;

                //visible effect of the progress
                GrowingEffect(progress);
                PlantGrown?.Invoke();

                isGrowing = false;
            }
        }
    }

    protected abstract void GrowingEffect(float progress);

    public abstract void Plant();

    /*     IEnumerator PlantingTween(){
            yield return null;
        } */

    public void Grow()
    {
        isGrowing = true;
        GrowInitThings();
    }

    public abstract void GrowInitThings();
}
