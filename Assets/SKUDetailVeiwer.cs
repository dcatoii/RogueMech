using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKUDetailVeiwer : MonoBehaviour {

    public PartPreviewer Preview;
    public RectTransform AttributeRoot;
    public RectTransform DetailRoot;
    public GameObject AttributeTextPrefab;
    public GameObject DetailTextPrefab;

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

        foreach (string detail in SKU.partPrefab.GetAttributeNamesForStore())
        {
            GameObject newDet = GameObject.Instantiate(AttributeTextPrefab, AttributeRoot);
            newDet.GetComponent<TMPro.TMP_Text>().text = detail;
        }

        //update previewer
        Preview.ChangePreviewObject(SKU.partPrefab.gameObject);
    }
}
