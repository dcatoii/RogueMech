using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LongbowHUD : WeaponHUD {

    Longbow longbow;
    public Slider ChargeGuage1;
    public Slider ChargeGuage2;
    public TMPro.TMP_Text ChargeLevelText;

    // Use this for initialization
    void Start()
    {
        longbow = (TrackedWeapon as Longbow);
        ChargeGuage1.maxValue = longbow.Level2Charge;
        ChargeGuage2.maxValue = longbow.Level3Charge;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ChargeLevelText.text = "Lv. " + (longbow.ChargeLevel + 1).ToString();
        if(longbow.IsCharging)
        {
            if (longbow.ChargeLevel == 0)
            {
                ChargeGuage1.value = longbow.EnergyLevel;
                ChargeGuage2.value = 0.0f;
            }
            else if (longbow.ChargeLevel == 1)
            {
                ChargeGuage1.value = longbow.Level2Charge;
                ChargeGuage2.value = longbow.EnergyLevel;
            }
            else if(longbow.ChargeLevel == 2)
            {
                ChargeGuage1.value = longbow.Level2Charge;
                ChargeGuage2.value = longbow.Level3Charge;
            }
        }
        else
        {
            ChargeGuage1.value = ChargeGuage2.value = 0.0f;
        }
    }
}
