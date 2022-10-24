using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TileWithGrownPlantState : TileState
{
    public TileWithGrownPlantState(TileController tc) : base(tc)
    {
    }

    public override TileState NextState()
    {
        tileController.EnableHitboxAfterDelay(1f);

        //Invoke(nameof(EnableHitboxColliderAfterDelay),0.75f);
        return new TileStartingState(tileController);
    }

    public override void OnHit()
    {
        if (gameController == null) gameController = GameController.Instance;

        if ((gameController.CurTileAction != GameController.TileAction.None) && (gameController.CurTileAction != GameController.TileAction.Harvest)) return;

        if (gameController.CurTileAction == GameController.TileAction.None)
        {
            gameController.SetTileAction(GameController.TileAction.Harvest);
        }//else the action is Harvest and proceed with the code without worries

        tileController.hitBoxCollider.enabled=false;

        tileController.Plant.OnHarvest();

        MonoBehaviour.Destroy(tileController.Plant.gameObject);

        tileController.GoToNextState();

      //  tileController.Plant.Plant();
       // tileController.Plant.PlantGrown += tileController.OnPlantGrown;
    }



}
