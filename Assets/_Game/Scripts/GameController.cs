using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController instance = null;
    public static GameController Instance { get => instance; }
    public static int CoinAmount { get => coinAmount; }
    public PlantController CurSelectedPlant { get => curSelectedPlant; set => curSelectedPlant = value; }
    public bool IsRaycastActive { get => isRaycastActive; set => isRaycastActive = value; }

    private static int coinAmount;

    #region Raycast variables
    private Ray ray;
    private RaycastHit hit;
    private GameObject hitObject;
    #endregion

    private bool isRaycastActive = true;

    private Camera mainCamera = null;

    public delegate void GameControllerEvent();

    public GameControllerEvent CoinAmountChanged;

    private PlantController curSelectedPlant = null;



    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRaycastActive)
        {
            if (Input.GetMouseButton(0))
            {
                ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    hitObject = hit.collider.gameObject;
                    if (hitObject.CompareTag("TileHitBox"))         //hitObject is a reference to HitBox here
                    {
                        TileHitBox tileHitBox = hitObject.GetComponent<TileHitBox>();
                        tileHitBox.OnHit();
                    }
                }
            }
        }
    }

    public void SetCoinAmount(int newCoinAmount)
    {
        coinAmount = newCoinAmount;
        CoinAmountChanged?.Invoke();
    }

    public void AddCoins(int coinAmountToAdd)
    {
        coinAmount += coinAmountToAdd;
        CoinAmountChanged?.Invoke();
    }
}
