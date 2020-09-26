using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MechHUD : MonoBehaviour {

    public Image Crosshairs;
    public MechEnergyHud EnergyHUD;
    public MechArmorHud ArmorHUD;
    WeaponHUD weaponHUD;

    private void FixedUpdate()
    {
        if (Mission.instance.PlayerFrame == null)
            return;

        if (EnergyHUD.TrackedCore == null)
        {
            EnergyHUD.TrackedCore = Mission.instance.PlayerFrame.Core;
        }
        if (ArmorHUD.TrackedCore == null)
        {
            ArmorHUD.TrackedCore = Mission.instance.PlayerFrame.Core;
        }
        if(weaponHUD == null)
        {
            GameObject newHudObject = GameObject.Instantiate(Mission.instance.PlayerFrame.RightHandWeapon.HUDPrefab.gameObject, transform);
            WeaponHUD newHUD = newHudObject.GetComponent<WeaponHUD>();
            weaponHUD = newHUD;
            newHUD.TrackedWeapon = Mission.instance.PlayerFrame.RightHandWeapon;

        }
    }
}
