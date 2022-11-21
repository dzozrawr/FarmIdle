using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TilesManager : MonoBehaviour
{

    public Transform circleForPlantingOrHarvesting = null;
    public CinemachineVirtualCamera plantCamera = null;

    public GameObject[] soilModels = null;

    private static string hexCodeOfColorOfWetSoil = "ABABAB";
    private static float soilDryingDuration=2f;
    private Color colorOfWetSoil; //ABABAB

    private Color defaultSoilColor;
    private List<TileController> tileControllers = null;

    private Coroutine drySoilCoroutine = null;

    private GameController gameController = null;

    private void Awake()
    {
        ColorUtility.TryParseHtmlString("#ABABAB", out colorOfWetSoil);

        defaultSoilColor = soilModels[0].GetComponent<Renderer>().material.color;
    }
    // Start is called before the first frame update
    void Start()
    {
        tileControllers = new List<TileController>();
        foreach (TileController tc in GetComponentsInChildren<TileController>())
        {
            tileControllers.Add(tc);
            tc.SetInteractable(false);    //we can maybe choose in the future the starting state of the tiles        
        }

        //when there are more than one tilesmanagers, the random one should be chosen for being the target for the guiding indicator (or closest one)
        gameController = GameController.Instance;
        gameController.playerController.guidingIndicator.SetTargetAndEnable(circleForPlantingOrHarvesting);
    }

    public void SetTilesInteractable(bool isInteractable)
    {
        foreach (TileController tc in tileControllers)
        {
            tc.SetInteractable(isInteractable);
        }
    }

    public void SetTilesWet()
    {
        foreach (GameObject soilModel in soilModels)
        {
            soilModel.GetComponent<Renderer>().material.color = colorOfWetSoil;
        }
        if (drySoilCoroutine == null)
            drySoilCoroutine = StartCoroutine(DrySoilOvertimeCoroutine(soilDryingDuration));
        else
        {
            StopCoroutine(drySoilCoroutine);
            drySoilCoroutine = StartCoroutine(DrySoilOvertimeCoroutine(soilDryingDuration));
        }

        foreach (TileController tc in tileControllers)
        {
            tc.WaterPlant();
        }
    }

    private IEnumerator DrySoilOvertimeCoroutine(float duration = 2f)
    {
        float timePassed = 0f;

        while (timePassed < duration)
        {
            yield return null;
            timePassed += Time.deltaTime;
            if (timePassed > duration) timePassed = duration;
            foreach (GameObject soilModel in soilModels)
            {
                soilModel.GetComponent<Renderer>().material.color = Color.Lerp(colorOfWetSoil, defaultSoilColor, timePassed / duration);
            }
        }
        drySoilCoroutine = null;
    }

}
