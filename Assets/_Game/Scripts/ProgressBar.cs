using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;

    private Vector3 compartmentCounterStartPos;
    private Vector3 compartmentCounterYOffset;

    private float targetValue = 0f;
    private float fillSpeed = 5f;

    private float prevSliderValue=-1f;

    public event Action OnProgressBarFilled;


    private void Awake()
    {
        slider.value = 0f;
    }

    public void SetMaxProgress(float progress)
    {
        slider.maxValue = progress;

        // compartmentCounterTxt.text = ((int)slider.value) + "/" + ((int)slider.maxValue);
    }


    public float GetMaxProgress()
    {
        return slider.maxValue;     //this is set to zero in the prefab
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetProgress(targetValue + 1);
        }
#endif
        prevSliderValue=slider.value;
        slider.value = Mathf.Lerp(slider.value, targetValue, fillSpeed * Time.deltaTime);

        if(((slider.value-prevSliderValue)<0.002f)&&(targetValue==slider.maxValue)){
            OnProgressBarFilled?.Invoke();
            this.enabled=false;
        }

        //slider.value = Mathf.Lerp();
    }

    public void SetProgress(float progress)
    {
        targetValue = progress;
        //  compartmentCounterTxt.text = ((int)progress) + "/" + ((int)slider.maxValue);
    }
}
