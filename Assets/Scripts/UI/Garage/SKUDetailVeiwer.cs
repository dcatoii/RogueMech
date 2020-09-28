using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SKUDetailVeiwer : MonoBehaviour {

    public PartPreviewer Preview;
    public TMPro.TMP_Text NameText;
    public RectTransform AttributeRoot;
    public RectTransform DetailRoot;
    public GameObject AttributeTextPrefab;
    public GameObject DetailTextPrefab;

    public GameObject EquipButton;
    public Button PurchaseButton;
    public TMPro.TMP_Text CostText;
    public TMPro.TMP_Text PurchaseText;
    public GameObject AlreadyEquipped;


    InventorySKU currentSKU = null;

    public void UpdateSKUDetails(InventorySKU SKU)
    {
        //clean up old attributes
        foreach (TMPro.TMP_Text attribute in AttributeRoot.GetComponentsInChildren<TMPro.TMP_Text>())
        {
            GameObject.Destroy(attribute.gameObject);
        }
        foreach (TMPro.TMP_Text detail in DetailRoot.GetComponentsInChildren<TMPro.TMP_Text>())
        {
            GameObject.Destroy(detail.gameObject);
        }

        //set new attributes and details
        foreach (string attribute in SKU.partPrefab.GetAttributeNamesForStore())
        {
            GameObject newAtt = GameObject.Instantiate(AttributeTextPrefab, AttributeRoot);
            newAtt.GetComponent<TMPro.TMP_Text>().text = attribute;
        }

        foreach (string detail in SKU.partPrefab.GetAttributeValuesForStore())
        {
            GameObject newDet = GameObject.Instantiate(DetailTextPrefab, DetailRoot);
            newDet.GetComponent<TMPro.TMP_Text>().text = detail;
        }

        //update name
        NameText.text = SKU.SkuID;

        //update previewer
        Preview.ChangePreviewObject(SKU.partPrefab.gameObject);

        currentSKU = SKU;

        //update UI
        PurchaseButton.gameObject.SetActive(currentSKU.isLocked);
        if (currentSKU.isLocked)
        {
            CostText.text = "Cost: $" + String.Format("{0:n0}", currentSKU.partPrefab.Cost);
            bool hasEnoughCash = PlayerData.Currency >= currentSKU.partPrefab.Cost;
            PurchaseButton.interactable = hasEnoughCash;
            PurchaseText.text = (hasEnoughCash ? "Purchase" : "Need More $$");
        }
        EquipButton.SetActive(!currentSKU.isLocked && !currentSKU.isEquipped);
        AlreadyEquipped.SetActive(!currentSKU.isLocked && currentSKU.isEquipped);

    }

    public void PurchaseButtonPressed()
    {
        if (PlayerData.Currency >= currentSKU.partPrefab.Cost)
        {
            currentSKU.isLocked = false;
            PlayerData.Currency -= currentSKU.partPrefab.Cost;
            PlayerData.UnlockPart(currentSKU.partPrefab.gameObject.name);
            PurchaseButton.gameObject.SetActive(false);
            EquipButton.SetActive(true);
        }
        
    }

    public void UpgradeButtonPressed()
    {

    }

    public void EquipButtonPressed()
    {
        switch(currentSKU.Catalogue.Category)
        {
            case (InventoryCatalogue.PartCategory.Legs):
                {
                    MechConstructor.instance.SwapLegs(currentSKU.LibraryID);
                    break;
                }
            case (InventoryCatalogue.PartCategory.Core):
                {
                    MechConstructor.instance.SwapCore(currentSKU.LibraryID);
                    break;
                }
            case (InventoryCatalogue.PartCategory.Arms):
                {
                    MechConstructor.instance.SwapArms(currentSKU.LibraryID);
                    break;
                }
            case (InventoryCatalogue.PartCategory.Head):
                {
                    MechConstructor.instance.SwapHead(currentSKU.LibraryID);
                    break;
                }
            case (InventoryCatalogue.PartCategory.Thruster):
                {
                    MechConstructor.instance.SwapThruster(currentSKU.LibraryID);
                    break;
                }
            case (InventoryCatalogue.PartCategory.Weapon_Right):
                {
                    MechConstructor.instance.SwapRightWeapon(currentSKU.LibraryID);
                    break;
                }
        }

        EquipButton.SetActive(false);
        AlreadyEquipped.SetActive(true);
    }
}
