using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesManager : MonoBehaviour
{

    public Transform circleForPlantingOrHarvesting=null;
    private List<TileController> tileControllers = null;

    private GameController gameController = null;
    // Start is called before the first frame update
    void Start()
    {
        tileControllers=new List<TileController>();
        foreach (TileController tc in GetComponentsInChildren<TileController>())
        {
            tileControllers.Add(tc);
            tc.SetInteractable(false);    //we can maybe choose in the future the starting state of the tiles        
        }

        //when there are more than one tilesmanagers, the random one should be chosen for being the target for the guiding indicator (or closest one)
        gameController=GameController.Instance;
        gameController.playerController.guidingIndicator.SetTargetAndEnable(circleForPlantingOrHarvesting);
    }

    public void SetTilesInteractable(bool isInteractable)
    {
        foreach (TileController tc in tileControllers)
        {
            tc.SetInteractable(isInteractable);
        }
    }

}
