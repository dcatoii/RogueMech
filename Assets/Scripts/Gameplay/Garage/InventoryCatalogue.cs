using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCatalogue : MonoBehaviour {

    public enum PartCategory
    {
        Misc = -1,
        Legs,
        Core,
        Arms,
        Head,
        Thruster,
        Weapon_Right,
        Weapon_Left
            
    };
    public PartCategory Category = PartCategory.Misc;
    public ToggleGroup Toggles;
    public List<InventorySKU> Skus;

    private void Start()
    {
        foreach(InventorySKU SKU in Skus)
        {
            SKU.Catalogue = this;
        }
    }


}
