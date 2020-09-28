using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextCounterInt : MonoBehaviour {

    public delegate void Callback();
    Callback OnCountReached = null;

    int targetValue;
    int startValue;
    int displayValue;
    public int DisplayValue { get { return displayValue; } }
    float timer = 0.0f;
    float completeTime = 0.0f;
    bool isCounting = false;

    public TMPro.TMP_Text Text;

    public void Count(int start, int end, float time, Callback callback)
    {
        startValue = start;
        targetValue = end;
        timer = 0.0f;
        completeTime = time;
        OnCountReached = callback;
        isCounting = true;
    }

    private void FixedUpdate()
    {
        if(isCounting)
        {
            timer += Time.fixedDeltaTime;
            if (timer >= completeTime)
            {
                displayValue = targetValue;
                isCounting = false;
                OnCountReached.Invoke();
                OnCountReached = null;
            }
            else
            {
                
                displayValue = startValue + (int)((targetValue - startValue) * (LeanTween.easeInOutQuad(0.0f, 1.0f, timer / completeTime)));
            }
            Text.text = String.Format("{0:n0}", displayValue);
        }
    }
}
