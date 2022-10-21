using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileStartingState : TileState
{
    public TileStartingState(TileController tc):base(tc){
    }
    public override void OnHit()
    {
        if(!GameController.Instance.CurSelectedPlant) return;

        tileController.hitBoxCollider.enabled=false;
        if (tileController.Plant == null)
        {
            tileController.Plant = MonoBehaviour.Instantiate(GameController.Instance.CurSelectedPlant, tileController.placeForPlant.position, Quaternion.identity, tileController.transform);
        }

        tileController.Plant.Plant();
        tileController.Plant.PlantGrown += tileController.OnPlantGrown;
    }

    public override TileState NextState(){
        tileController.hitBoxCollider.enabled=true;
        return new TileWithGrownPlantState(tileController);
    }
}
