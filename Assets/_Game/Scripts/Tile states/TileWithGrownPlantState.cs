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
        MonoBehaviour.Destroy(tileController.Plant.gameObject);

        Debug.Log("+5 coins!");

      //  tileController.Plant.Plant();
       // tileController.Plant.PlantGrown += tileController.OnPlantGrown;
    }


}
