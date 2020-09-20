using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKUDetailVeiwer : MonoBehaviour {

    public PartPreviewer Preview;
    public TMPro.TMP_Text NameText;
    public RectTransform AttributeRoot;
    public RectTransform DetailRoot;
    public GameObject AttributeTextPrefab;
    public GameObject DetailTextPrefab;

    public GameObject EquipButton;
    public GameObject PurchaseButton;

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
    }

    public void PurchaseButtonPressed()
    {

    }

    public void EquipButtonPressed()
    {
        switch(currentSKU.Catalogue.Category)
        {
            case (InventoryCatalogue.PartCategory.Legs):
                {
                    MechConstructor.instance.SwapLegs(currentSKU.partPrefab as FrameLegs);
                    break;
                }
            case (InventoryCatalogue.PartCategory.Core):
                {
                    MechConstructor.instance.SwapCore(currentSKU.partPrefab as FrameCore);
                    break;
                }
            case (InventoryCatalogue.PartCategory.Arms):
                {
                    MechConstructor.instance.SwapArms(currentSKU.partPrefab as FrameArms);
                    break;
                }
            case (InventoryCatalogue.PartCategory.Head):
                {
                    MechConstructor.instance.SwapHead(currentSKU.partPrefab as FrameHead);
                    break;
                }
        }
    }
}
