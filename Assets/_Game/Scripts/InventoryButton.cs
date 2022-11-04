using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    public PlantController plant=null;

    private GameController gameController=null;

    private void Start() {
        gameController=GameController.Instance;
    }


    public void OnClick(){
        Debug.Log(" OnClick()");
       // gameController.CurSelectedPlant=plant;
    }
}
