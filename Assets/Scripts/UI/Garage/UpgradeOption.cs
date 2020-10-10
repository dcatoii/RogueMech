using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeOption : MonoBehaviour {

    public Toggle toggle;
    public Image StoreCard;
    public int UpgradeLevel;
    public int UpgradeIndex;
    public string UpgradeName;
    public string UpgradeDescription;


    public void OnToggle()
    {
        if (toggle.isOn)
            SendMessageUpwards("SelectedUpgradeOption", this);
    }

}
