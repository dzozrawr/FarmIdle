using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WindmillScript : MonoBehaviour
{
    private static int maxLvl=4;
    private int lvl = 0;

    

    private int moneyIncrement = 2;

    private float moneyIncTime = 3f;

    private float timer = 0f;
    private bool isMoneyMakingActive = false;

    private GameController gameController = null;

    private int lvlUpPrice = 20;
    public Canvas canvasForLvlUp = null;

    public EndlessRotate endlessRotateOfBlades = null;

    public CoinUIEarnScript moneyWorldUI = null;

    public TMP_Text priceText=null;
    
    //public GameObject lvlUp



    // Start is called before the first frame update
    void Start()
    {
        gameController = GameController.Instance;
        priceText.text=lvlUpPrice+"";
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
        if(lvl<maxLvl)
        canvasForLvlUp.enabled = true;//enable lvlUP button

    }

    public void LevelUp()
    {
        if (lvlUpPrice > GameController.CoinAmount) return;
        gameController.AddMoney(-lvlUpPrice);
        SoundManager.Instance.PlaySound("coinClaim");

        switch (lvl)
        {
            case 0:
                endlessRotateOfBlades.enabled = true; //start rotation of blades
                isMoneyMakingActive = true;//start money making over time
                //moneyWorldUI.SetMoneyAmount(moneyIncrement);
                lvl++;
                break;
            case 1:
                moneyIncTime/=2f;
                //moneyIncrement = 3;
                lvl++;
                break;
            case 2:
                moneyIncrement = 4;
                lvl++;
                break;
            case 3:
                moneyIncTime/=2f;
                //moneyIncrement = 5;
                lvl++;
                break;
            default:
                //  endlessRotateOfBlades.rotSpeed *= 2f;
                break;
        }
        moneyWorldUI.SetMoneyAmount(moneyIncrement);
        endlessRotateOfBlades.rotSpeed *= 2f;
        lvlUpPrice *= 2;
        priceText.text=lvlUpPrice+"";
        canvasForLvlUp.enabled=false;

        //set UI to show new price
    }

    private void OnTriggerExit(Collider other)
    {
        canvasForLvlUp.enabled = false; //disable lvlUP button
    }
}
