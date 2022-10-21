using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileWithGrownPlantState : TileState
{
    public TileWithGrownPlantState(TileController tc) : base(tc)
    {
    }

    public override TileState NextState()
    {
        return null;
    }

    public override void OnHit()
    {
        tileController.hitBoxCollider.enabled=false;

        tileController.Plant.OnHarvest();

        MonoBehaviour.Destroy(tileController.Plant.gameObject);

        

      //  tileController.Plant.Plant();
       // tileController.Plant.PlantGrown += tileController.OnPlantGrown;
    }


}
