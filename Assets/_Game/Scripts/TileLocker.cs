using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class TileLocker : MonoBehaviour
{
    public bool isLocked = true;   //all the tiles are unlocked by default

    public Canvas lockedUI = null;

    public int price = 0;

    public TMP_Text priceText = null;

    public CinemachineVirtualCamera newCameraView=null;

    public TilesManager tilesManager=null;

    private List<TileController> tileControllers = null;

    private GameController gameController = null;
    private CameraController cameraController= null;

    private void Awake()
    {
        priceText.text = price + "";
    }
    // Start is called before the first frame update
    void Start()
    {
         cameraController = CameraController.Instance;
        if (isLocked)
        {
            lockedUI.gameObject.SetActive(true);
            if (tileControllers == null) tileControllers = new List<TileController>();
            foreach (TileController tc in GetComponentsInChildren<TileController>())
            {
                tileControllers.Add(tc);
                tc.Lock();
            }
        }else{
            lockedUI.gameObject.SetActive(false);
        }
    }

    public void Unlock()
    {
        if (GameController.CoinAmount >= price)
        {
            if (gameController == null) gameController = GameController.Instance;
            gameController.AddMoneyIncrementally(-price);
        }else{
            return;
        }

        lockedUI.gameObject.SetActive(false);

        foreach (TileController tc in tileControllers)
        {
            tc.Unlock();
        }

        if(newCameraView!=null){
            tilesManager.plantCamera=newCameraView;
            if(tilesManager.IsInPlantHarvestMode){
               cameraController.TransitionToCMVirtualCamera(tilesManager.plantCamera);
            }
        }
    }
}
