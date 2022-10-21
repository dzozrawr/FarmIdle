using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Aezakmi.Tweens;
using DG.Tweening;

public class CoinUIForPlant : MonoBehaviour
{
    public GameObject coinTextGO = null;

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

        canvas = GetComponent<Canvas>();


        animationDuration = tweener.LoopDuration;
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

    }

    public void PlayCoinEarnAnimationAndDie(int coinAmount)
    {
        transform.SetParent(transform.parent.parent);

        coinText.text = "+" + coinAmount;

        canvas.enabled = true;
        tweener.AddDelegateOnComplete(() =>
        {
            GameController.Instance.AddCoins(coinAmount);
            Destroy(gameObject);
            //tweener.RemoveDelegateOnComplete()
        });
        tweener.PlayTween();
        StartCoroutine(FadeInTween(coinTextCanvasGroup, animationDuration));
    }
}
