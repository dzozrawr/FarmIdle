using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesManager : MonoBehaviour
{
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
    }

    public void SetTilesInteractable(bool isInteractable)
    {
        foreach (TileController tc in tileControllers)
        {
            tc.SetInteractable(isInteractable);
        }
    }

}
