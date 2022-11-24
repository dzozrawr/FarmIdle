using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    public PlantController plant = null;
    public PlantInfo.PlantType type;
    public GameObject unselectedBacgkround = null;
    public GameObject selectedBackground = null;

    private GameController gameController = null;
    private ShopController shopController = null;

    private ShopController.ShopItemInfo shopItemInfo=null;

    private void Start()
    {
        gameController = GameController.Instance;
        shopController = ShopController.Instance;

        shopItemInfo= shopController.FindShopItemInfoByType(type);
        if(shopItemInfo!=null){
            if(shopItemInfo.isLocked){
                shopController.ShopItemBoughtEvent+=OnShopItemBought;//add listener for the bought event
                gameObject.SetActive(false);
            }
        }
    }

    private void OnDestroy() {
        if(shopItemInfo!=null && shopItemInfo.isLocked){    //unsubscribe from event just in case if I haven't been bought
            shopController.ShopItemBoughtEvent-=OnShopItemBought;
        }
    }
    private void OnShopItemBought(PlantInfo.PlantType type){
        if(type==this.type){
            gameObject.SetActive(true);
            shopController.ShopItemBoughtEvent-=OnShopItemBought;
        }
    }


    public void OnClick()
    {
        if (selectedBackground.activeSelf)
        {//if I'm already selected deselect me
            SetSelected(false);
            gameController.CurSelectedPlant = null;
            return;
        }

        SetSelected(true);
        gameController.CurSelectedPlant = plant;

        foreach (InventoryButton iB in transform.parent.GetComponentsInChildren<InventoryButton>())
        {
            if (iB != this)
            {
                iB.SetSelected(false);
            }
        }
        // gameController.CurSelectedPlant=plant;
    }

    public void SetSelected(bool shouldSelect)
    {
        selectedBackground.SetActive(shouldSelect);
        unselectedBacgkround.SetActive(!shouldSelect);
    }
}
