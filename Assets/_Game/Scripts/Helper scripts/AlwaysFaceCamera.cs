using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysFaceCamera : MonoBehaviour
{

    private Camera mainCamera=null;

    private void Awake() {
        mainCamera=Camera.main;
    }


    // Update is called once per frame
    void Update()
    {
        transform.forward=mainCamera.transform.forward;
    }
}
