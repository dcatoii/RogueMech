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
    public InventorySKU SKUPrefab;

    private void Start()
    {
        foreach(InventorySKU SKU in Skus)
        {
            SKU.Catalogue = this;
        }
    }

    public void GenerateSKUs(FramePart[] parts)
    {
        int LibID = 0;
        foreach (FramePart part in parts)
        {
            GameObject newSKUObj = GameObject.Instantiate(SKUPrefab.gameObject, transform);
            InventorySKU newSKU = newSKUObj.GetComponent<InventorySKU>();
            newSKU.Catalogue = this;
            newSKU.toggle.group = Toggles;
            newSKU.SkuID = part.name;
            newSKU.partPrefab = part;
            newSKU.PreviewModel = part.gameObject;
            newSKU.StoreImage = part.StoreImage;
            newSKU.LibraryID = LibID;
            LibID++;

            Skus.Add(newSKU);
        }
    }

    protected void FixedUpdate()
    {
        FramePart equippedPart = ApplicationContext.PlayerCustomizationData.GetPartByType(Category);

        foreach(InventorySKU sku in Skus)
        {
            sku.isEquipped = (sku.partPrefab == equippedPart);
        }
    }
}
