using System.Collections;
using System.Collections.Generic;
using Aezakmi.Tweens;
using UnityEngine;
using DG.Tweening;

public abstract class PlantController : MonoBehaviour
{

    public delegate void PlantHarvestedHandler(PlantInfo.PlantType plantType);
    public static event PlantHarvestedHandler PlantHarvested;
    private static float timeToScaleOutOnHarvest=0.1f;
    public PlantInfo.PlantType type;
    public int coinWorth = 0;
    
    public float timeNeededToGrow = 1f;

    

    public float marketScaleBy=0.75f;

    public ParticleSystem readyForHarvestParticles=null;

    public ParticleSystem harvestParticles=null;

    public delegate void Notify();

    public event Notify PlantGrown;
    protected bool isGrowing = false;

    protected float growingCurTime = 0f;
    protected float progress = 0f;

    protected GameController gameController=null;






    // Update is called once per frame
    protected void Update()
    {
        if (isGrowing)
        {
            progress = growingCurTime / timeNeededToGrow;

            //visible effect of the progress
            GrowingEffect(progress);    //could be scaling, could be animation, could be anything

            growingCurTime += Time.deltaTime;

            if (growingCurTime >= timeNeededToGrow)
            {
                progress = 1f;

                //visible effect of the progress
                GrowingEffect(progress);
                PlantGrown?.Invoke();

                isGrowing = false;

                if(readyForHarvestParticles!=null) readyForHarvestParticles.Play();
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

    public void BoostProgressBy(float perc){
        progress=(progress+perc)>1f?1f:(progress+perc);
        growingCurTime=progress*timeNeededToGrow;
    }

    public abstract void GrowInitThings();

    public abstract void OnHarvestSpecific();
    public void OnHarvest()
    {
        transform.DOScale(Vector3.zero,timeToScaleOutOnHarvest).OnComplete(()=>{
            Destroy(gameObject);
        });
        OnHarvestSpecific();
        if(harvestParticles!=null){
            Vector3 localScale= harvestParticles.transform.localScale;
            harvestParticles.transform.SetParent(null);
            harvestParticles.transform.localScale=localScale;
            harvestParticles.Play(); //play harvest particles
        } 
        PlantHarvested?.Invoke(type);
    }

    public abstract GameObject GetGrownPlantModel();
}
