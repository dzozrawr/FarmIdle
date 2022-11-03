using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCanvasController : MonoBehaviour
{
    public Canvas canvas=null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCanvasVisible(bool isVisible){
        canvas.enabled=isVisible;
    }
}
