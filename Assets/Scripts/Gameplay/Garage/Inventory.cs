using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public TMPro.TMP_Dropdown CategoryDropdown;
    public List<InventoryCatalogue> Catalogues;
    public ScrollRect InventoryScroller;

    public SKUDetailVeiwer DetailViewer;

    Dictionary<InventoryCatalogue.PartCategory, InventoryCatalogue> SortedInventory;

    private void Start()
    {
        SortedInventory = new Dictionary<InventoryCatalogue.PartCategory, InventoryCatalogue>();
        CategoryDropdown.options.Clear();
        foreach (InventoryCatalogue catalogue in Catalogues)
        {
            SortedInventory.Add(catalogue.Category, catalogue);
            CategoryDropdown.options.Add(new TMPro.TMP_Dropdown.OptionData(catalogue.Category.ToString()));
        }

        //this terrible code is intended to force a refresh of the values on startup
        CategoryDropdown.value = 1;
        CategoryDropdown.value = 0;
    }

    public void UpdateActiveCatalogue()
    {
        foreach (InventoryCatalogue catalogue in Catalogues)
        {
            if (catalogue.Category.ToString() == CategoryDropdown.options[CategoryDropdown.value].text)
            {
                catalogue.gameObject.SetActive(true);
                InventoryScroller.content = catalogue.GetComponent<RectTransform>();
            }
            else
                catalogue.gameObject.SetActive(false);
        }
    }

    void SelectedKUChanged(InventorySKU newSKU)
    {
        DetailViewer.UpdateSKUDetails(newSKU);
    }

}
