using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightmareHUD : WeaponHUD {
    Nightmare nightmare;
    public Slider ChargeGuage;

    // Use this for initialization
    void Start()
    {
        nightmare = (TrackedWeapon as Nightmare);
        ChargeGuage.maxValue = nightmare.ChargeDelay;
    }

    // Update is called once per frame
    void Update()
    {
        ChargeGuage.value = nightmare.ChargeTime;
    }
}
