using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensitivityOptionControl : MonoBehaviour {

    public Slider SenseSlider;
    public TMPro.TMP_InputField SenseInput;

    private void Start()
    {
        SenseSlider.value = PlayerData.Sensitivity;
        SenseInput.text = PlayerData.Sensitivity.ToString();
    }

    public void OnSensitivityChangedText()
    {
        float value = float.Parse(SenseInput.text);
        value = Mathf.Clamp(value, 0.1f, 100f);
        PlayerData.Sensitivity = value;
        SenseSlider.value = value;
        SenseInput.text = value.ToString();
    }
    public void OnSensitivityChangedSlider()
    {
        float value = SenseSlider.value;
        value = Mathf.Clamp(value, 0.1f, 100f);
        PlayerData.Sensitivity = value;
        SenseSlider.value = value;
        SenseInput.text = value.ToString(); ;
    }
}
