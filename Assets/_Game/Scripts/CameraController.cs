using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zoomOutMin = 1;
    public float zoomOutMax = 8f;

    private Vector3 touchStart;

    private Camera cam = null;

    private GameController gameController=null;
    private void Awake()
    {
        cam = GetComponent<Camera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        gameController=GameController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = cam.ScreenToWorldPoint(Input.mousePosition);
        }



        if (Input.touchCount == 2)
        {
            gameController.IsRaycastActive=false; //disable raycast to stop interacting with the world

            Vector3 direction = touchStart - cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position += direction;

            Touch touchZero= Input.GetTouch(0);
            Touch touchOne= Input.GetTouch(1);

            Vector2 touchZeroPrevPos= touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos= touchOne.position - touchOne.deltaPosition;
            
            float prevMagnitude= (touchZeroPrevPos -touchOnePrevPos).magnitude;
            float curMagnitude= (touchZero.position -touchOne.position).magnitude;

            float difference= curMagnitude-prevMagnitude;

            Zoom(difference*0.01f);
        }else{
             gameController.IsRaycastActive=true; //disable raycast to stop interacting with the world
        }


        #if UNITY_EDITOR

        if (Input.GetMouseButtonDown(2))
        {
            touchStart = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 direction = touchStart - cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position += direction;
        }

         Zoom(Input.GetAxis("Mouse ScrollWheel"));
#endif
       
    }

    private void Zoom(float increment)
    {
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }
}
