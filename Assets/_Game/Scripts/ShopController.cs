using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    private static ShopController instance = null;
    public static ShopController Instance { get => instance; }

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

    public delegate void ShopItemBoughtHandler(PlantInfo.PlantType type);

    public event ShopItemBoughtHandler ShopItemBoughtEvent;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        if (shopItemInfos == null)  //a similair check goes on when loading
        {
            shopItemInfos = new Dictionary<PlantInfo.PlantType, ShopItemInfo>();
            foreach (ShopItem item in shopItems)
            {
                ShopItemInfo shopItemInfo = new ShopItemInfo();
                shopItemInfo.type = item.type;
                //locked by default
                shopItemInfo.price = item.PriceInt;

                shopItemInfos.Add(item.type, shopItemInfo);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    public ShopItemInfo FindShopItemInfoByType(PlantInfo.PlantType type)
    {
        if (shopItemInfos != null)
        {
            ShopItemInfo shopItemInfo = null;
            try
            {
                shopItemInfo = shopItemInfos[type];
            }
            catch (KeyNotFoundException k)
            {
                shopItemInfo=null;
            }
            return shopItemInfo;
        }
        else
            return null;
    }

    public void BuyShopItem(PlantInfo.PlantType type){
        shopItemInfos[type].isLocked=false;
        ShopItemBoughtEvent?.Invoke(type);
        //fire an event for it to be unlocked
    }


    public void SetCanvasVisible(bool isVisible)
    {
        canvas.enabled = isVisible;
    }
}