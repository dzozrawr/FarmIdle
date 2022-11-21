using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItem : MonoBehaviour
{
    public PlantInfo.PlantType type;
    public TMP_Text priceText=null;

    private int priceInt;

    public int PriceInt { get => priceInt; set => priceInt = value; }

    private void Awake() {
        priceInt=int.Parse(priceText.text);  
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
}
