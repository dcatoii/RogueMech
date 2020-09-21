using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MechHUD : MonoBehaviour {

    public Image Crosshairs;
    public MechEnergyHud EnergyHUD;
    public MechArmorHud ArmorHUD;

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
    }
}
