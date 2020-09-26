using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChainsawHUD : WeaponHUD {
    TexasChainsaw chainsaw;
    public Slider HeatGuage;
    public GameObject OverheatBanner;

    // Use this for initialization
    void Start()
    {
        chainsaw = (TrackedWeapon as TexasChainsaw);
        HeatGuage.maxValue = chainsaw.MaxHeat;
    }

    // Update is called once per frame
    void Update()
    {
        HeatGuage.value = chainsaw.CurrentHeat;
        OverheatBanner.SetActive(chainsaw.IsOverheated);
    }
}
