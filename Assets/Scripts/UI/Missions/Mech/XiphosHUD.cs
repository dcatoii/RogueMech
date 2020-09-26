using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XiphosHUD : WeaponHUD {

    Xiphos xiphos;
    public Slider[] charges;

	// Use this for initialization
	void Start () {
        xiphos = (TrackedWeapon as Xiphos);
	}
	
	// Update is called once per frame
	void Update () {

        for (int ii = 0; ii < xiphos.MaxCharges; ii++)
        {
            if (ii < xiphos.Charges)
            {
                charges[ii].value = 1.0f;
            }
            else if (ii == xiphos.Charges)
            {
                charges[ii].value = xiphos.Recharge / xiphos.RechargeCooldown;
            }
            if (ii > xiphos.Charges)
            {
                charges[ii].value = 0.0f;
            }
        }
	}
}
