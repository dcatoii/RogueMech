using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public MechPartLibrary PartLibrary;
    public TMPro.TMP_Dropdown CategoryDropdown;
    public List<InventoryCatalogue> Catalogues;
    public ScrollRect InventoryScroller;

    public InventoryCatalogue CataloguePrefab;

    public SKUDetailVeiwer DetailViewer;

    Dictionary<InventoryCatalogue.PartCategory, InventoryCatalogue> SortedInventory;

    private void Start()
    {
        CreateAllCatalogues();
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

    void CreateAllCatalogues()
    {
        if (Catalogues != null)
        {
            foreach (InventoryCatalogue catalogue in Catalogues)
                GameObject.Destroy(catalogue.gameObject);
            Catalogues.Clear();
        }
        else
            Catalogues = new List<InventoryCatalogue>();


        CreateCatalogue(InventoryCatalogue.PartCategory.Head, PartLibrary.Heads);
        CreateCatalogue(InventoryCatalogue.PartCategory.Core, PartLibrary.Cores);
        CreateCatalogue(InventoryCatalogue.PartCategory.Arms, PartLibrary.Arms);
        CreateCatalogue(InventoryCatalogue.PartCategory.Legs, PartLibrary.Legs);
        CreateCatalogue(InventoryCatalogue.PartCategory.Thruster, PartLibrary.Thrusters);
        CreateCatalogue(InventoryCatalogue.PartCategory.Weapon_Right, PartLibrary.Weapons);


    }

    void CreateCatalogue(InventoryCatalogue.PartCategory category, FramePart[] parts)
    {
        GameObject newCatalogueObj = GameObject.Instantiate(CataloguePrefab.gameObject, InventoryScroller.viewport);
        InventoryCatalogue newCatalogue = newCatalogueObj.GetComponent<InventoryCatalogue>();
        newCatalogueObj.name = "Catalogue_" + category.ToString();
        newCatalogue.Category = category;
        newCatalogue.GenerateSKUs(parts);
        Catalogues.Add(newCatalogue);
    }
}
