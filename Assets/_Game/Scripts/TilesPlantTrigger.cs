using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class TilesPlantTrigger : MonoBehaviour
{

    public TilesManager tilesManager = null;
    private CameraController cameraController = null;
    private GameController gameController = null;

    private PlayerController playerController = null;

    private Button checkmarkButton = null;

    private Coroutine triggerStayTimer = null;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        cameraController = CameraController.Instance;
        gameController = GameController.Instance;
        playerController = gameController.playerController;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("OnTriggerEnter");
        if (!playerController.HasBucketOfWater)
        {
            TransitionToPlantHarvestMode();
        }
        else
        {
            playerController.SetBucketExistence(false);
            //playerController.HasBucketOfWater=false;
            tilesManager.SetTilesWet();
            SoundManager.Instance.PlaySound("wateringSound");
            //            Debug.Log("Bucket emptied");
        }
    }

    private void TransitionToPlantHarvestMode()
    {
        cameraController.TransitionToCMVirtualCamera(tilesManager.plantCamera);
        CheckForCameraBlending.onCameraBlendFinished += OnCameraTransitionToPlantsFinished;

        gameController.playerController.SetJoystickEnabledAndVisible(false);
        tilesManager.IsInPlantHarvestMode = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!tilesManager.IsInPlantHarvestMode)
        {
            if (triggerStayTimer == null) triggerStayTimer = StartCoroutine(TriggerStayTimer());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (triggerStayTimer != null)
        {
            StopCoroutine(triggerStayTimer);
            triggerStayTimer = null;
        }
    }

    IEnumerator TriggerStayTimer()
    {
        float duration = 2f;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        TransitionToPlantHarvestMode();

        triggerStayTimer = null;
    }

    private void OnCameraTransitionToPlantsFinished()
    {
        tilesManager.SetTilesInteractable(true);
        //   Debug.Log("Activate tiles");    //to be implemented
        //gameController.playerController.SetJoystickCanvasEnabled(false);
        gameController.inventoryCanvas.enabled = true;

        if (checkmarkButton == null) checkmarkButton = gameController.inventoryCanvasController.checkmarkButton;

        checkmarkButton.onClick.AddListener(OnCheckmarkButtonClicked);


        CheckForCameraBlending.onCameraBlendFinished -= OnCameraTransitionToPlantsFinished;
    }

    private void OnCheckmarkButtonClicked()
    {
        tilesManager.SetTilesInteractable(false);
        // Debug.Log("Deactivate tiles");    //to be implemented
        gameController.inventoryCanvas.enabled = false;
        cameraController.TransitionToCMVirtualCamera(gameController.playerController.followingCamera);
        CheckForCameraBlending.onCameraBlendFinished += OnCameraTransitionToPlayerFinished;

        tilesManager.IsInPlantHarvestMode = false;


        checkmarkButton.onClick.RemoveAllListeners();
    }

    private void OnCameraTransitionToPlayerFinished()
    {
        gameController.playerController.SetJoystickEnabledAndVisible(true);

        if (gameController.playerController.backpackPlantsList == null || gameController.playerController.backpackPlantsList.Count == 0)
        {   //and if player planted something point at well
            // gameController.playerController.guidingIndicator.SetTargetAndEnable(transform);
            gameController.playerController.guidingIndicator.SetTargetAndEnable(gameController.wellScript.transform);
        }
        else
        {
            gameController.playerController.guidingIndicator.SetTargetAndEnable(gameController.market.transform);
        }

        CheckForCameraBlending.onCameraBlendFinished -= OnCameraTransitionToPlayerFinished;
    }

}
