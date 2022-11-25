using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WindmillScript : MonoBehaviour
{
    private static int maxLvl = 4;
    private static int lvl = 0; //this is static, because the windmill will retain its level through scenes



    private int moneyIncrement = 2;

    private float moneyIncTime = 3f;

    private float timer = 0f;
    private bool isMoneyMakingActive = false;

    private GameController gameController = null;

    private int lvlUpPrice = 20;
    public Canvas canvasForLvlUp = null;

    public EndlessRotate endlessRotateOfBlades = null;

    public CoinUIEarnScript moneyWorldUI = null;

    public TMP_Text priceText = null;

    //public GameObject lvlUp
    public GameObject[] windmillModels = null;


    // Start is called before the first frame update
    void Start()
    {
        gameController = GameController.Instance;
        priceText.text = lvlUpPrice + "";

        if (lvl > 0)
        {
            SetWindmillToLevel(lvl);
        }
    }

    private void SetWindmillToLevel(int level)
    {
        isMoneyMakingActive = true;//start money making over time
        switch (lvl)
        {

            case 4:
                moneyIncTime /= 2f;
            //moneyIncrement = 5;
                goto case 3;
            case 3:
                moneyIncrement = 4;
                /*                 Aezakmi.Tweens.Scale scaleOfWindmill0=windmillModels[0].GetComponent<Aezakmi.Tweens.Scale>();
                                scaleOfWindmill0.AddDelegateOnComplete(()=>{
                                    windmillModels[0].SetActive(false);
                                });
                                scaleOfWindmill0.PlayTween(); */

                windmillModels[0].SetActive(false);

                /*                 Aezakmi.Tweens.Scale scaleOfWindmill1=windmillModels[1].GetComponent<Aezakmi.Tweens.Scale>();
                                windmillModels[1].transform.localScale=Vector3.zero; */
                windmillModels[1].SetActive(true);

                EndlessRotate endlessRotateOfBlades1 = windmillModels[1].GetComponentInChildren<EndlessRotate>();
                endlessRotateOfBlades1.rotSpeed = endlessRotateOfBlades.rotSpeed;
                endlessRotateOfBlades = endlessRotateOfBlades1;

            //  scaleOfWindmill1.PlayTween();
            //scaleOfWindmill1.AddDelegateOnComplete(()=>{windmillModels[1].GetComponentInChildren<Collider>().enabled=true;});
            //new model here
                goto case 2;
            case 2:
                moneyIncTime /= 2f;


                break;
            default:
                //  endlessRotateOfBlades.rotSpeed *= 2f;
                break;
        }

        moneyWorldUI.SetMoneyAmount(moneyIncrement);
        endlessRotateOfBlades.rotSpeed *= Mathf.Pow(2,lvl);
        lvlUpPrice *= (int) Mathf.Pow(2,lvl);
        priceText.text = lvlUpPrice + "";

        if (lvl >= maxLvl) canvasForLvlUp.enabled = false;
    }

    private void Update()
    {
        if (isMoneyMakingActive)
        {
            timer += Time.deltaTime;
            if (timer >= moneyIncTime)
            {
                gameController.AddMoney(moneyIncrement);
                moneyWorldUI.PlayCoinEarnAnimation();//visual effect for making money
                timer = 0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        /*         if(lvl<maxLvl)
                canvasForLvlUp.enabled = true;//enable lvlUP button */

    }

    public void LevelUp()
    {
        if (lvlUpPrice > GameController.CoinAmount) return;
        gameController.AddMoney(-lvlUpPrice);
        SoundManager.Instance.PlaySound("coinClaim");

        switch (lvl)
        {
            case 0:
                // endlessRotateOfBlades.enabled = true; //start rotation of blades
                isMoneyMakingActive = true;//start money making over time
                                           //moneyWorldUI.SetMoneyAmount(moneyIncrement);

                break;
            case 1:
                moneyIncTime /= 2f;
                //moneyIncrement = 3;

                break;
            case 2:
                moneyIncrement = 4;
                Aezakmi.Tweens.Scale scaleOfWindmill0 = windmillModels[0].GetComponent<Aezakmi.Tweens.Scale>();
                scaleOfWindmill0.AddDelegateOnComplete(() =>
                {
                    windmillModels[0].SetActive(false);
                });
                scaleOfWindmill0.PlayTween();


                Aezakmi.Tweens.Scale scaleOfWindmill1 = windmillModels[1].GetComponent<Aezakmi.Tweens.Scale>();
                windmillModels[1].transform.localScale = Vector3.zero;
                windmillModels[1].SetActive(true);

                EndlessRotate endlessRotateOfBlades1 = windmillModels[1].GetComponentInChildren<EndlessRotate>();
                endlessRotateOfBlades1.rotSpeed = endlessRotateOfBlades.rotSpeed;
                endlessRotateOfBlades = endlessRotateOfBlades1;

                scaleOfWindmill1.PlayTween();
                //scaleOfWindmill1.AddDelegateOnComplete(()=>{windmillModels[1].GetComponentInChildren<Collider>().enabled=true;});
                //new model here
                break;
            case 3:
                moneyIncTime /= 2f;
                //moneyIncrement = 5;

                break;
            default:
                //  endlessRotateOfBlades.rotSpeed *= 2f;
                break;
        }
        lvl++;
        moneyWorldUI.SetMoneyAmount(moneyIncrement);
        endlessRotateOfBlades.rotSpeed *= 2f;
        lvlUpPrice *= 2;
        priceText.text = lvlUpPrice + "";
        if (lvl >= maxLvl) canvasForLvlUp.enabled = false;

        //set UI to show new price
    }

    private void OnTriggerExit(Collider other)
    {
        //  canvasForLvlUp.enabled = false; //disable lvlUP button
    }
}
