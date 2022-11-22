using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Aezakmi.Tweens;
using DG.Tweening;

public class CoinUIForPlant : MonoBehaviour
{
    public GameObject coinTextGO = null;

    private Vector3 coinTextStartingPos;

    private int moneyAmount = 2;

    private TMP_Text coinText = null;
    private TweenBase tweener = null;

    private CanvasGroup coinTextCanvasGroup = null;

    private float animationDuration = 0f;

    private Canvas canvas = null;


    private void Awake()
    {
        coinText = coinTextGO.GetComponent<TMP_Text>();
        tweener = coinTextGO.GetComponent<TweenBase>();
        coinTextCanvasGroup = coinTextGO.GetComponent<CanvasGroup>();

        coinTextStartingPos = coinTextGO.transform.position;

        canvas = GetComponent<Canvas>();


        animationDuration = tweener.LoopDuration;

        coinText.text = moneyAmount + "";
    }

    public void SetMoneyAmount(int amount)
    {
        moneyAmount = amount;
        coinText.text ="+" + moneyAmount;
    }

    IEnumerator FadeInTween(CanvasGroup cg, float duration)
    {
        float t = 0f;

        while (t <= duration)
        {
            cg.alpha = t / duration;
            yield return null;
            t += Time.deltaTime;
        }

        cg.alpha = 1;
        SetMoneyUIToDefaultState();
    }

    private void SetMoneyUIToDefaultState()
    {
        coinTextCanvasGroup.alpha = 0f;
        coinTextGO.transform.position = coinTextStartingPos;
    }

    public void PlayCoinEarnAnimation()
    {
        // transform.SetParent(transform.parent.parent);

        coinText.text = "+" + moneyAmount;

        canvas.enabled = true;
        tweener.AddDelegateOnComplete(() =>
        {

        });
        tweener.PlayTween();
        StartCoroutine(FadeInTween(coinTextCanvasGroup, animationDuration));
    }

    public void PlayCoinEarnAnimation(int coinAmount)
    {
        transform.SetParent(transform.parent.parent);

        coinText.text = "+" + coinAmount;

        canvas.enabled = true;
        tweener.AddDelegateOnComplete(() =>
        {
            // GameController.Instance.AddMoneyIncrementally(coinAmount);
            //Destroy(gameObject);
            //tweener.RemoveDelegateOnComplete()
        });
        tweener.PlayTween();
        StartCoroutine(FadeInTween(coinTextCanvasGroup, animationDuration));
    }
}
