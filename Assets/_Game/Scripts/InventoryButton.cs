using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    public PlantController plant = null;
    public GameObject unselectedBacgkround = null;
    public GameObject selectedBackground = null;

    private GameController gameController = null;



    private void Start()
    {
        gameController = GameController.Instance;
    }


    public void OnClick()
    {
        if (selectedBackground.activeSelf)
        {//if I'm already selected deselect me
            SetSelected(false);
            gameController.CurSelectedPlant=null;
            return;
        }

        SetSelected(true);
        gameController.CurSelectedPlant=plant;
        
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
