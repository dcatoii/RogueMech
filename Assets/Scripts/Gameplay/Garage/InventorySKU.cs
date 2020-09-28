using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySKU : MonoBehaviour {

    public string SkuID;
    public FramePart partPrefab;
    public Sprite StoreImage;
    public GameObject PreviewModel;

    public bool isLocked = true;
    public bool isEquipped = false;

    public int LibraryID;

    public Image StoreCard;
    public GameObject LockedIcon;
    public GameObject EquippedIcon;
    public Toggle toggle;

    public InventoryCatalogue Catalogue;

    private void Start()
    {
        StoreCard.sprite = StoreImage;
        isLocked = !PlayerData.IsPartUnlocked(partPrefab.gameObject.name);
    }

    public void OnToggle()
    {
        if(toggle.isOn)
            SendMessageUpwards("SelectedSKUChanged",this);
    }

    protected void FixedUpdate()
    {
        EquippedIcon.SetActive(isEquipped);
        LockedIcon.SetActive(isLocked);
    }
}
