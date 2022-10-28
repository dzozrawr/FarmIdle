using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderController : MonoBehaviour
{

    [System.Serializable]
    public class OrderElement
    {
        public PlantInfo.PlantType plantType;
        public int quantity;
        public int coinRewardAmount;
        public Sprite plantSprite;
    }

    public List<OrderElement> orders = null;

    public OrderUIElement orderUIElement = null;

    private OrderElement curOrder = null;

    private int plantsHarvestedN = 0;

    private void Awake()
    {
        if ((orders != null) && (orders.Count > 0))
        {
            curOrder = orders[0];
            orders.RemoveAt(0);
            orderUIElement.InitOrder(curOrder);
        }

        PlantController.PlantHarvested += OnPlantHarvested;
    }


    private void OnDestroy()
    {
        PlantController.PlantHarvested -= OnPlantHarvested;
    }

    public void OnPlantHarvested(PlantInfo.PlantType plantType)
    {
        if(curOrder==null) return;
        if (plantsHarvestedN == curOrder.quantity)
        {
            return;
        }

        if (curOrder.plantType == plantType)
        {
            plantsHarvestedN++;
            orderUIElement.UpdateProgress(plantsHarvestedN);
        }

        if (plantsHarvestedN == curOrder.quantity)
        {
            orderUIElement.completedOrderButton.onClick.AddListener(OnCompletedOrderButtonClicked);
            orderUIElement.SetCompletedState(true);
            // Debug.Log("Transform order into a button");
        }
    }

    public void OnCompletedOrderButtonClicked()
    {
        GameController.Instance.AddCoins(curOrder.coinRewardAmount);
        orderUIElement.completedOrderButton.gameObject.SetActive(false);

        Invoke(nameof(GoToNextOrder),0.75f);

        orderUIElement.completedOrderButton.onClick.RemoveListener(OnCompletedOrderButtonClicked);
    }

    private void SetToDefaultState()
    {
        curOrder = null;
        plantsHarvestedN = 0;
    }

    private void GoToNextOrder()
    {
        SetToDefaultState();
        if ((orders != null) && (orders.Count > 0))
        {
            orderUIElement.SetToDefaultState();

            curOrder = orders[0];
            orders.RemoveAt(0);
            orderUIElement.InitOrder(curOrder);
        }
    }

}
