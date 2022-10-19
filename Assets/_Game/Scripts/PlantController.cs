using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    private bool isGrowing = false;
    private float growingMaxTime = 1f;
    private float growingTime = 0f;

    private float startingScale, goalScale = 0.3f;

    private float progress = 0f;

    private float curScale = -1f;

    public delegate void Notify();

    public event Notify PlantGrown;
    private void Awake()
    {
        startingScale = gameObject.transform.localScale.x;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isGrowing)
        {
            progress = growingTime / growingMaxTime;

            //visible effect of the progress
            GrowingEffect(progress);    //could be scaling, could be animation, could be anything

            growingTime += Time.deltaTime;

            if (growingTime >= growingMaxTime)
            {
                progress = 1f;

                //visible effect of the progress
                GrowingEffect(progress);
                PlantGrown?.Invoke();

                isGrowing = false;
            }
        }
    }

    protected void GrowingEffect(float progress)
    {
        Debug.Log(progress);
        curScale = startingScale + (goalScale - startingScale) * progress;
        gameObject.transform.localScale = new Vector3(curScale, curScale, curScale);
    }

    public void Grow()
    {
        isGrowing = true;
    }
}
