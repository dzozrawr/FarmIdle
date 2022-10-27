using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class TilesPlantTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera plantTopDownCamera = null;
    public TilesManager tilesManager=null;
    private CameraController cameraController = null;
    private GameController gameController = null;

    private Button checkmarkButton = null;


    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        cameraController = CameraController.Instance;
        gameController = GameController.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        cameraController.TransitionToCMVirtualCamera(plantTopDownCamera);
        CheckForCameraBlending.onCameraBlendFinished += OnCameraTransitionToPlantsFinished;

        gameController.playerController.SetJoystickEnabledAndVisible(false);
    }

    private void OnCameraTransitionToPlantsFinished()
    {
        tilesManager.SetTilesInteractable(true);
     //   Debug.Log("Activate tiles");    //to be implemented
        //gameController.playerController.SetJoystickCanvasEnabled(false);
        gameController.inventoryCanvas.enabled = true;

        if(checkmarkButton==null) checkmarkButton = gameController.inventoryCanvasController.checkmarkButton;

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


        checkmarkButton.onClick.RemoveAllListeners();
    }

    private void OnCameraTransitionToPlayerFinished()
    {
        gameController.playerController.SetJoystickEnabledAndVisible(true);

        CheckForCameraBlending.onCameraBlendFinished -= OnCameraTransitionToPlayerFinished;
    }

}
