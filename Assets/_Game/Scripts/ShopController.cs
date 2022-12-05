using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

public class ShopController : MonoBehaviour
{
    private static ShopController instance = null;
    public static ShopController Instance { get => instance; }

    [DataContract]
    public class ShopItemInfo
    {
        [DataMember]
        public PlantInfo.PlantType type;
        [DataMember]
        public bool isLocked = true;
        [DataMember]
        public int price;
    }

    public static List<ShopItemInfo> shopItemInfos = null;
    public Canvas canvas = null;

    public ShopItem[] shopItems = null;

    public delegate void ShopItemBoughtHandler(PlantInfo.PlantType type);

    public event ShopItemBoughtHandler ShopItemBoughtEvent;

    private GameController gameController = null;

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
            shopItemInfos = new List<ShopItemInfo>();
            foreach (ShopItem item in shopItems)
            {
                ShopItemInfo shopItemInfo = new ShopItemInfo();
                shopItemInfo.type = item.type;
                //locked by default
                shopItemInfo.price = item.PriceInt;

                shopItemInfos.Add(shopItemInfo);
            }
        }
        else
        {
            ShopItemInfo s=null;
            foreach (ShopItem item in shopItems)
            {
                s=FindShopItemInfoByType(item.type);
                if (!s.isLocked)
                {
                    item.SetBought();
                    ShopItemBoughtEvent?.Invoke(item.type);
                }
            }
        }
    }

    

    private void Start()
    {
        gameController = GameController.Instance;
    }

    public void OnBackButtonClicked()
    {
        canvas.enabled = false;
        gameController.IsRaycastActive = true;
    }


    public ShopItemInfo FindShopItemInfoByType(PlantInfo.PlantType type)
    {
        if (shopItemInfos != null)
        {
            ShopItemInfo shopItemInfo = null;

            foreach (ShopItemInfo s in shopItemInfos)
            {
                if(s.type==type){
                    shopItemInfo=s;
                    break;
                } 
            }

            return shopItemInfo;
        }
        else
            return null;
    }

    public void BuyShopItem(PlantInfo.PlantType type)
    {
        ShopItemInfo shopItemInfo= FindShopItemInfoByType(type);
        shopItemInfo.isLocked = false;
        ShopItemBoughtEvent?.Invoke(type);  //fire an event for it to be unlocked

        SaveData saveData = new SaveData();
        SaveSystem.SaveGameAsyncXML(saveData);
    }


    public void SetCanvasVisible(bool isVisible)
    {
        canvas.enabled = isVisible;
    }

    public void SetShopItemVisible(PlantInfo.PlantType type, bool isVisible)
    {
        foreach (ShopItem item in shopItems)
        {
            if (item.type == type)
            {
                item.SetVisible(isVisible);
            }
        }
    }
}
