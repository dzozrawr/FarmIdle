using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MarketScript : MonoBehaviour
{
    public Canvas canvasForLvlUp = null;

    public Collider colliderException = null;

    public TMP_Text priceText=null;

    public GameObject[] marketModels=null;

    private static int maxLvl = 1;

    private int lvl = 0;
    private int lvlUpPrice = 30;

    private GameController gameController = null;


    // Start is called before the first frame update
    void Start()
    {
        gameController = GameController.Instance;
        priceText.text=lvlUpPrice+"";
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other == colliderException) return;
        if (lvl < maxLvl)
            canvasForLvlUp.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == colliderException) return;
        canvasForLvlUp.enabled = false;
    }

    public void LevelUp()
    {
        if (lvlUpPrice > GameController.CoinAmount) return;
        gameController.AddMoney(-lvlUpPrice);
        SoundManager.Instance.PlaySound("coinClaim");

        switch (lvl)
        {
            case 0:
                marketModels[0].GetComponentInChildren<Collider>().enabled=false;
                marketModels[1].GetComponentInChildren<Collider>().enabled=false;

                Aezakmi.Tweens.Scale scaleOfMarket0=marketModels[0].GetComponent<Aezakmi.Tweens.Scale>();
                scaleOfMarket0.AddDelegateOnComplete(()=>{
                    marketModels[0].SetActive(false);
                });
                scaleOfMarket0.PlayTween();


                Aezakmi.Tweens.Scale scaleOfMarket1=marketModels[1].GetComponent<Aezakmi.Tweens.Scale>();
                marketModels[1].transform.localScale=Vector3.zero;
                marketModels[1].SetActive(true);
                scaleOfMarket1.PlayTween();
                scaleOfMarket1.AddDelegateOnComplete(()=>{marketModels[1].GetComponentInChildren<Collider>().enabled=true;});
                //scale out the lvl1 market model and scale in the lvl2 market model (maybe disable the selling functionality meanwhile)
                lvl++;
                break;
        }
      //  moneyWorldUI.SetMoneyAmount(moneyIncrement);
       // endlessRotateOfBlades.rotSpeed *= 2f;
    //    lvlUpPrice *= 2;
     //   priceText.text = lvlUpPrice + "";
        canvasForLvlUp.enabled = false;

        //set UI to show new price
    }
}
// kurac xaaxxaxaxaxaaaxaxxa
