using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItem : MonoBehaviour
{
    public PlantInfo.PlantType type;
    public TMP_Text priceText=null;

    private int priceInt;
    private ShopController shopController=null;

    public int PriceInt { get => priceInt; set => priceInt = value; }

    private void Awake() {
        priceInt=int.Parse(priceText.text);  
    }

    private void Start() {
        shopController=ShopController.Instance;
    }

    public void OnClick(){
        if(priceInt<=GameController.CoinAmount){
            GameController.Instance.AddMoneyIncrementally(-priceInt);   //reduce the money in game controller
            shopController.BuyShopItem(type);//do the buying logic in shop controller
            priceText.transform.parent.gameObject.SetActive(false); //apply the bought visual
        }
    }
}
