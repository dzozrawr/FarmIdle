using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidingIndicator : MonoBehaviour
{
    public Canvas indicatorCanvas=null;
    public Transform indicatorToControl=null;

    private bool isEnabled=false;

    private Transform target=null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if((isEnabled)&&(target!=null)){
            indicatorToControl.LookAt(target);
            indicatorToControl.forward-=new Vector3(0,indicatorToControl.forward.y,0);

            if(Vector3.Distance(indicatorToControl.position,target.position)<2f){
                SetEnabled(false);
            }
            //indicatorToControl.LookAt()
        }
    }

    public void SetEnabled(bool shouldEnable){
        indicatorCanvas.enabled=shouldEnable;        
        this.isEnabled=shouldEnable;
    }

    public void SetTargetAndEnable(Transform t){
        target=t;
        SetEnabled(true);
    }
}
