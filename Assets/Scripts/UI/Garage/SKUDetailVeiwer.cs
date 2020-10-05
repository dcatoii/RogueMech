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
    public RectTransform UpgradeRoot;

    public GameObject StatsRoot;
    public GameObject AttributeTextPrefab;
    public GameObject DetailTextPrefab;

    public UpgradePanel UpgradePanelPrefab;

    public GameObject EquipButton;
    public Button PurchaseButton;
    public TMPro.TMP_Text CostText;
    public TMPro.TMP_Text PurchaseText;
    public GameObject AlreadyEquipped;

    InventorySKU currentSKU = null;
    List<UpgradePanel> upgradePanels;

    public void UpdateSKUDetails(InventorySKU SKU)
    {
        

        

        //update previewer
        Preview.ChangePreviewObject(SKU.partPrefab.gameObject);

        currentSKU = SKU;

        ///////////////////update UI///////////////////////
        //update name
        NameText.text = SKU.SkuID;
        //update stats
        UpdateStats();

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

        
        UpdateUpgradeTab();
    }

    private void UpdateStats()
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
        foreach (string attribute in Preview.Part.GetAttributeNamesForStore())
        {
            GameObject newAtt = GameObject.Instantiate(AttributeTextPrefab, AttributeRoot);
            newAtt.GetComponent<TMPro.TMP_Text>().text = attribute;
        }

        foreach (string detail in Preview.Part.GetAttributeValuesForStore())
        {
            GameObject newDet = GameObject.Instantiate(DetailTextPrefab, DetailRoot);
            newDet.GetComponent<TMPro.TMP_Text>().text = detail;
        }
    }

    public void PurchaseButtonPressed()
    {
        if (PlayerData.Currency >= currentSKU.partPrefab.Cost)
        {
            currentSKU.isLocked = false;
            PlayerData.Currency = PlayerData.Currency - currentSKU.partPrefab.Cost;
            PlayerData.UnlockPart(currentSKU.partPrefab.PartID);
            PurchaseButton.gameObject.SetActive(false);
            AlreadyEquipped.SetActive(false);
            EquipButton.SetActive(true);
            UpdateUpgradeTab();
        }
        
    }

    public void UpgradeButtonPressed()
    {
        UpgradeData data = UpgradeManager.LoadUpgradeData(currentSKU.partPrefab);

        if (PlayerData.Currency >= currentSKU.partPrefab.UpgradeLevels[data.UpgradeLevel].Cost)
        {
            //consume currency
            PlayerData.Currency = PlayerData.Currency - currentSKU.partPrefab.UpgradeLevels[data.UpgradeLevel].Cost;
            //upgrae the part
            UpgradeManager.UnlockUpgradeLevel(currentSKU.partPrefab);
            //apply the first upgrade option
            UpgradeManager.SwapUpgrade(Preview.Part, data.UpgradeLevel, 0);
            //update the stats and upgrade panels
            UpdateStats();
            UpdateUpgradeTab();
            //update preview mech
            MechConstructor.instance.UpdateUpgrades();

        }
    }

    public void SelectedUpgradeOption(UpgradeOption option)
    {
        UpgradeManager.SwapUpgrade(Preview.Part, option.UpgradeLevel, option.UpgradeIndex);
        UpgradeManager.ApplyUpgrades(Preview.Part);

        //Fixes a null ref issue with the mech constructor's start not being called first
        if (MechConstructor.instance != null)
            MechConstructor.instance.UpdateUpgrades();

        //update part stats
        UpdateStats();
        //do not need to update upgrade tab becuase the panel handels options changing on it's own
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

    private void UpdateUpgradeTab()
    {
        //clear the old panels
        if (upgradePanels != null)
            foreach (UpgradePanel panel in upgradePanels)
                GameObject.Destroy(panel.gameObject);

        upgradePanels = new List<UpgradePanel>();

        UpgradeData data = UpgradeManager.LoadUpgradeData(currentSKU.partPrefab);
        int currLevel = 0;
        foreach(UpgradeLevel upgrade in Preview.Part.UpgradeLevels)
        {
            GameObject newPanelObj = GameObject.Instantiate(UpgradePanelPrefab.gameObject, UpgradeRoot);
            UpgradePanel panel = newPanelObj.GetComponent<UpgradePanel>();
            panel.UpgradeLevel = currLevel;
            panel.GenerateOptions(upgrade.Options);

            //if the part is locked, indicate that the part must be purchased on the first upgrade. Simply mark the rest as locked
            if (currentSKU.isLocked)
            {
                panel.ShowLockedView();
                if (currLevel == 0)
                {
                    panel.LockedText.text = "Purchase this part from the store to unlock upgrades";
                }
                else
                {
                    panel.LockedText.text = "Locked";
                }
            }
            else
            {
                //if this upgrade is already unlocked
                if (currLevel < data.UpgradeLevel)
                {
                    panel.ShowUnlockedView();
                    if (data.UpgradeOptions[currLevel] != -1)
                    {
                        panel.Options[data.UpgradeOptions[currLevel]].toggle.isOn = true;
                        panel.SelectedUpgradeOption(panel.Options[data.UpgradeOptions[currLevel]]);
                    }
                    else //no upgrade has been selected for this level
                    {
                        panel.UpgradeNameText.text = "Choose an Upgrade";
                        panel.UpgradeDescriptionText.text = "";
                    }
                }
                //if this is the next available upgrade tier
                else if (currLevel == data.UpgradeLevel)
                {
                    panel.ShowPurchaseableView();
                    panel.UnlockCostText.text = "Unlock Upgrade Tier: $" + String.Format("{0:n0}", upgrade.Cost);
                }
                //if this is 1 or more tiers below the next available upgrade
                else
                {
                    panel.ShowLockedView();
                    panel.LockedText.text = "You must purchase all previous upgrade tiers before this upgrade becomes available";
                }
            }
            //add to the panel list
            upgradePanels.Add(panel);
            //increment level
            currLevel++;
        }
    }
}
