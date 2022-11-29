using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public GameControllerEvent MoneyAmountChangedInc, MoneyAmountChanged;

    public ShopTrigger market = null;

    public List<Transform> plantTriggerCircles = null;

    public int coinsToCompleteLevel = 200;

    public ProgressBar progressBar = null;

    public EOLCanvasController EOLCanvasController = null;



    #region Raycast variables
    private Ray ray;
    private RaycastHit hit;
    private GameObject hitObject;
    #endregion

    private bool isRaycastActive = true;

    private Camera mainCamera = null;

    private PlantController curSelectedPlant = null;

    private TileAction curTileAction = TileAction.None;

    private int moneyEarnedInLevel = 0;

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
        progressBar.SetMaxProgress(coinsToCompleteLevel);
        MoneyAmountChangedInc += UpdateProgressOnCoinAmountChanged;
    }

    private void UpdateProgressOnCoinAmountChanged()
    {
        if (moneyEarnedInLevel >= coinsToCompleteLevel)
        {
            progressBar.SetProgress(coinsToCompleteLevel);
            progressBar.OnProgressBarFilled += ShowEOLCanvas;
            //EOLCanvasController.GetComponent<Canvas>().enabled = true;
            //  Debug.Log("Activate button to go to next level");
        }
        else
        {
            progressBar.SetProgress(moneyEarnedInLevel);
        }

    }

    private void ShowEOLCanvas()
    {
        EOLCanvasController.GetComponent<Canvas>().enabled = true;
        progressBar.OnProgressBarFilled -= ShowEOLCanvas;
    }

    // Update is called once per frame
    void Update()
    {


#if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.E))
        {
            AddMoneyIncrementally(20);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            int nextSceneIndex = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
            SceneManager.LoadScene(nextSceneIndex);
        }

#endif


        if (isRaycastActive)
        {

            if (Input.GetMouseButton(0))
            {
                if (IsOverRaycastBlockingUI()) return;   //this condition is here, because it is checking on a lot of things and would cause poorer performance if put higher in the update loop
                ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    hitObject = hit.collider.gameObject;
                    if (hitObject.CompareTag("TileHitBox"))         //hitObject is a reference to HitBox here
                    {
                        //if (IsOverRaycastBlockingUI()) return;   //this condition is here, because it is checking on a lot of things and would cause poorer performance if put higher in the update loop
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

    public void SetCoinAmountIncerementally(int newCoinAmount)
    {
        coinAmount = newCoinAmount;
        MoneyAmountChangedInc?.Invoke();
    }

    public void AddMoneyIncrementally(int coinAmountToAdd)
    {
        coinAmount += coinAmountToAdd;
        if ((moneyEarnedInLevel + coinAmountToAdd) < 0) moneyEarnedInLevel = 0; else moneyEarnedInLevel += coinAmountToAdd;
        MoneyAmountChangedInc?.Invoke();
    }

    public void AddMoney(int coinAmountToAdd)
    {
        coinAmount += coinAmountToAdd;
        if ((moneyEarnedInLevel + coinAmountToAdd) < 0) moneyEarnedInLevel = 0; else moneyEarnedInLevel += coinAmountToAdd;
        MoneyAmountChanged?.Invoke();
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

    public static bool IsOverRaycastBlockingUI()
    {
        int id = 0;
#if UNITY_EDITOR
        id = -1;
#endif
        //  bool isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(id);       //this checks if the pointer is over UI (through EventSystem) and if it is then it blocks raycasts
        bool isOverBlockingUI =
                                UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(id) &&
                                UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject != null &&
                                UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.CompareTag("UIRayBlock");       //this checks if the pointer is over UI (through EventSystem) and if it is then it blocks raycasts

        return isOverBlockingUI;
    }

    //public void SetPlanting
}
