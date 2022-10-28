using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OrderUIElement : MonoBehaviour
{
    public TMP_Text numbersText=null;
    public Image orderImage=null;

    public GameObject orderParent=null;

    public Button completedOrderButton=null;

    private int quantity=-1;

    public void InitOrder(OrderController.OrderElement o){
        quantity=o.quantity;
        
        numbersText.text="0/"+quantity;
        orderImage.sprite=o.plantSprite;
    }

    public void UpdateProgress(int plantsHarvestedN){
        numbersText.text=plantsHarvestedN+"/"+quantity;
    }

    public void SetCompletedState(bool isCompleted){
        completedOrderButton.gameObject.SetActive(isCompleted);
        orderParent.SetActive(!isCompleted);
    }

    public void SetToDefaultState(){
        completedOrderButton.gameObject.SetActive(false);
        numbersText.text="";
        orderImage.sprite=null;
        orderParent.SetActive(true);
    }
}
