using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TilesPlantTrigger : MonoBehaviour
{
    private CameraController cameraController=null;
    public CinemachineVirtualCamera plantTopDownCamera=null;

    private void Awake() {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        cameraController=CameraController.Instance;
    }

    private void OnTriggerEnter(Collider other) {
        cameraController.TransitionToCMVirtualCamera(plantTopDownCamera);
        CheckForCameraBlending.onCameraBlendFinished+=OnCameraTransitionFinished;
    }

    private void OnCameraTransitionFinished(){
        Debug.Log("Activate tiles");
        CheckForCameraBlending.onCameraBlendFinished-=OnCameraTransitionFinished;
    }

}
