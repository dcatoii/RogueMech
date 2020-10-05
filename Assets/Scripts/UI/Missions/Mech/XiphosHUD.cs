using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XiphosHUD : WeaponHUD {

    Xiphos xiphos;
    public Slider chargePrefab;
    public Slider[] charges;
    public Transform ChargeRoot;
    public float minChargeWidth;
    public float maxChargeWidth;

    // Use this for initialization
    void Start()
    {
        xiphos = (TrackedWeapon as Xiphos);
        if (xiphos == null)
        {
            return;
        }
        charges = new Slider[xiphos.MaxCharges];
        //used to calculate maximum array index for placement and size
        int maxIndex = xiphos.MaxCharges - 1;
        for (int ii = 0; ii < xiphos.MaxCharges; ii++)
        {
            GameObject newChargeObj = GameObject.Instantiate(chargePrefab.gameObject, ChargeRoot);
            Slider newCharge = newChargeObj.GetComponent<Slider>();
            RectTransform sliderRect = newCharge.GetComponent<RectTransform>();
            int chargeIndex = maxIndex - ii;
            sliderRect.sizeDelta = new Vector2(Mathf.Lerp(minChargeWidth, maxChargeWidth, (float)(ii)/(float)(maxIndex)) , sliderRect.sizeDelta.y);
            charges[chargeIndex] = newCharge;
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (xiphos == null)
            return;

        for (int ii = 0; ii < xiphos.MaxCharges; ii++)
        {
            if (ii < xiphos.Charges)
            {
                charges[ii].value = 1.0f;
            }
            else if (ii == xiphos.Charges)
            {
                charges[ii].value = xiphos.Recharge / xiphos.BulletRechargeTime;
            }
            if (ii > xiphos.Charges)
            {
                charges[ii].value = 0.0f;
            }
        }
	}
}
