using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Raycast variables
    private Ray ray;
    private RaycastHit hit;
    private GameObject hitObject;
    #endregion

    private bool isRaycastActive=true;

    private Camera mainCamera=null;

    private void Awake() {
        mainCamera=Camera.main;
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
                        TileHitBox tileHitBox= hitObject.GetComponent<TileHitBox>();
                        tileHitBox.OnHit();
                    }
                }
            }
        }
    }
}
