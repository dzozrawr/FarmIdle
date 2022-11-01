using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileStartingState : TileState
{
    public TileStartingState(TileController tc) : base(tc)
    {
    }
    public override void OnHit()
    {
        if (gameController == null) gameController = GameController.Instance;

        if (!gameController.CurSelectedPlant) return;

        if ((gameController.CurTileAction != GameController.TileAction.None) && (gameController.CurTileAction != GameController.TileAction.Plant)) return;

        if (gameController.CurTileAction == GameController.TileAction.None)
        {
            gameController.SetTileAction(GameController.TileAction.Plant);
        }//else the action is plant and proceed with the code without worries

        //Debug.Log("if (gameController.CurSelectedPlant) return;");



        tileController.hitBoxCollider.enabled = false;
        if (tileController.Plant == null)
        {
            tileController.Plant = MonoBehaviour.Instantiate(GameController.Instance.CurSelectedPlant, tileController.placeForPlant.position, Quaternion.identity, tileController.transform);
        }

        tileController.Plant.Plant();
        SoundManager.Instance.PlaySoundWPitchChange("plantingSound");
        tileController.Plant.PlantGrown += tileController.OnPlantGrown;
    }

    public override TileState NextState()
    {
        tileController.hitBoxCollider.enabled = true;
        return new TileWithGrownPlantState(tileController);
    }
}
