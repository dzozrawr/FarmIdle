using System.Collections;
using System.Collections.Generic;
using Aezakmi.Tweens;
using UnityEngine;
using DG.Tweening;

public class MultiModelPlant : PlantController
{
    private float startingScale, goalScale = 0.3f; //special case

    private float curScale = -1f; //special case

    public TweenBase tweenForPlanting = null; //special case

    public GameObject[] models = null;

    public Canvas progressCanvas = null; //special case

    public ProgressCircle progressCircle = null; //special case

    public CoinUIForPlant coinUIForPlant = null;
    public override void GrowInitThings()
    {
        startingScale = models[0].transform.localScale.x;
        tweenForPlanting.RemoveDelegateOnComplete(Grow);
    }

    public override void Plant()
    {
        //effect of planting
        GameObject plantModel = models[0];
        float startingScale = plantModel.transform.localScale.x * 0.75f;
        plantModel.transform.localScale = new Vector3(plantModel.transform.localScale.x * 0.75f, plantModel.transform.localScale.y * 0.75f, plantModel.transform.localScale.z * 0.75f);

        tweenForPlanting.PlayTween();
        tweenForPlanting.AddDelegateOnComplete(Grow);



        progressCanvas.enabled = true;
    }

    IEnumerable FadeInTween(CanvasGroup cg, float duration)
    {
        float t = 0f;

        while (t <= duration)
        {
            cg.alpha = t / duration;
            yield return null;
            t += Time.deltaTime;
        }

        cg.alpha = 1;

    }

    protected override void GrowingEffect(float progress)
    {
        //Debug.Log(progress);
        if (progress > 0.99f)
        {
            models[1].SetActive(false);
            models[2].SetActive(true);
        }
        else if (progress >= 0.5f)
        {
            models[0].SetActive(false);
            models[1].SetActive(true);
        }

        progressCircle.SetProgress(progress);
    }

    public override void OnHarvestSpecific()
    {
        if (gameController == null) gameController = GameController.Instance;
        gameController.playerController.AddPlantToBackpack(new PlantInfo(type, coinWorth),GetGrownPlantModel());
        //coinUIForPlant.PlayCoinEarnAnimationAndDie(coinWorth);
    }

    public override GameObject GetGrownPlantModel()
    {
        return models[models.Length - 1];
    }

}
