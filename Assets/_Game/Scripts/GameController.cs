using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum TileAction
    {
        None, Plant, Harvest
    }

    private static GameController instance = null;
    private static int coinAmount;
    public static GameController Instance { get => instance; }
    public static int CoinAmount { get => coinAmount; }


    public delegate void TileActionChangeHandler(TileAction newTileAction);
    public event TileActionChangeHandler OnTileActionChanged;



    public PlayerController playerController = null;
    public Canvas inventoryCanvas = null;

    public InventoryCanvasController inventoryCanvasController = null;

    public delegate void GameControllerEvent();

    public GameControllerEvent CoinAmountChanged;

    public ShopTrigger market = null;

    public List<Transform> plantTriggerCircles = null;



    #region Raycast variables
    private Ray ray;
    private RaycastHit hit;
    private GameObject hitObject;
    #endregion

    private bool isRaycastActive = true;

    private Camera mainCamera = null;

    private PlantController curSelectedPlant = null;

    private TileAction curTileAction = TileAction.None;
    public TileAction CurTileAction { get => curTileAction; }
    public PlantController CurSelectedPlant { get => curSelectedPlant; set => curSelectedPlant = value; }
    public bool IsRaycastActive { get => isRaycastActive; set => isRaycastActive = value; }

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

    private void Start()
    {
        //playerController.guidingIndicator.SetTargetAndEnable();
    }

    // Update is called once per frame
    void Update()
    {


#if UNITY_EDITOR

    if(Input.GetKeyDown(KeyCode.E)){
        AddCoins(20);
    }

#endif


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
            else //if mouse button is not held down
            {
                if (CurTileAction != TileAction.None) SetTileAction(TileAction.None);
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

    public void SetTileAction(TileAction newTileAction)
    {
        if (newTileAction == curTileAction) return;

        curTileAction = newTileAction;
        OnTileActionChanged?.Invoke(newTileAction);
    }

    public Transform GetClosestPlantTriggerCircle(Transform from)
    {
        float minDistance = float.MaxValue;
        Transform closestCircle = null;

        float distance = 0f;
        foreach (Transform t in plantTriggerCircles)
        {
            distance = Vector3.Distance(from.position, t.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestCircle = t;
            }
        }

        return closestCircle;
    }

    //public void SetPlanting
}
