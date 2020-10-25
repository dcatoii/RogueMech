﻿using System.Collections;
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
        //Inform the gamestate that the garage has been initialized and ensure the cursor is usable
        ApplicationContext.Game.CurrentState = GameContext.Gamestate.Garage;
        ApplicationContext.UnlockCursor();
        ApplicationContext.Game.IsPaused = false;
    }

    public void UpdateActiveCatalogue()
    {
        foreach (InventoryCatalogue catalogue in Catalogues)
        {
            if (catalogue.Category.ToString() == CategoryDropdown.options[CategoryDropdown.value].text)
            {
                catalogue.gameObject.SetActive(true);
                InventoryScroller.content = catalogue.GetComponent<RectTransform>();
                catalogue.Skus[0].toggle.isOn = true;
                catalogue.Skus[0].OnToggle();
            }
            else
                catalogue.gameObject.SetActive(false);
        }
    }

    void SelectedSKUChanged(InventorySKU newSKU)
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
    private void FixedUpdate()
    {
        if (ApplicationContext.Game.IsPaused)
            return;

        if (Input.GetAxis("Cancel") > 0)
        {
            ApplicationContext.OpenPauseMenu();
        }
    }
}
