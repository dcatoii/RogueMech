using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour {

    public UpgradeOption OptionPrefab;
    public int UpgradeLevel;
    public GameObject LockedView;
    public GameObject PurchasableView;
    public GameObject UnlockedView;
    public GameObject OptionContainer;
    public ToggleGroup Toggles;
    public TMPro.TMP_Text UpgradeNameText;
    public TMPro.TMP_Text UpgradeDescriptionText;
    public TMPro.TMP_Text UnlockCostText;
    public TMPro.TMP_Text LockedText;
    public List<UpgradeOption> Options;

    public void ShowLockedView()
    {
        LockedView.SetActive(true);
        PurchasableView.SetActive(false);
        UnlockedView.SetActive(false);
    }

    public void ShowUnlockedView()
    {
        LockedView.SetActive(false);
        PurchasableView.SetActive(false);
        UnlockedView.SetActive(true);
    }

    public void ShowPurchaseableView()
    {
        LockedView.SetActive(false);
        PurchasableView.SetActive(true);
        UnlockedView.SetActive(false);

    }
    public void UnlockButtonPressed()
    {
        SendMessageUpwards("UpgradeButtonPressed");
    }

    public void SelectedUpgradeOption(UpgradeOption option)
    {
        Debug.Log("SelectedUpgradeOption " + "Level: " + option.UpgradeLevel.ToString() + " Index: " + option.UpgradeIndex.ToString());
        UpgradeNameText.text = option.UpgradeName;
        UpgradeDescriptionText.text = option.UpgradeDescription;
    }

    public void GenerateOptions(PartUpgrade[] upgrades)
    {
        Options = new List<UpgradeOption>();

        int index = 0;
        foreach(PartUpgrade upgrade in upgrades)
        {
            GameObject optionObj = GameObject.Instantiate(OptionPrefab.gameObject, OptionContainer.transform);
            UpgradeOption newOption = optionObj.GetComponent<UpgradeOption>();
            newOption.toggle.group = Toggles;
            newOption.UpgradeLevel = UpgradeLevel;
            newOption.UpgradeIndex = index;
            newOption.StoreCard.sprite = upgrade.StoreImage;
            newOption.UpgradeName = upgrade.name;
            newOption.UpgradeDescription = upgrade.StoreDescription;
            Options.Add(newOption);

            index++;
        }
    }
}
