using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    //public enum PlantT
    public class ShopItemInfo
    {
        public PlantInfo.PlantType type;
        public bool isLocked = true;
        public int price;
    }

    public Dictionary<PlantInfo.PlantType, ShopItemInfo> shopItemInfos = null;
    public Canvas canvas = null;

    public ShopItem[] shopItems = null;

    private void Awake()
    {
        if (shopItemInfos == null)  //a similair check goes on when loading
        {
            shopItemInfos=new Dictionary<PlantInfo.PlantType, ShopItemInfo>();
            foreach (ShopItem item in shopItems)
            {
                ShopItemInfo shopItemInfo=new ShopItemInfo();
                shopItemInfo.type=item.type;
                //locked by default
                shopItemInfo.price=item.PriceInt;

                shopItemInfos.Add(item.type,shopItemInfo);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }


    public void SetCanvasVisible(bool isVisible)
    {
        canvas.enabled = isVisible;
    }
}
