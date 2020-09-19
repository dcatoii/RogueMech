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

    public Image StoreCard;
    public Toggle toggle;

    private void Start()
    {
        StoreCard.sprite = StoreImage;
    }

    public void OnToggle()
    {
        if(toggle.isOn)
            SendMessageUpwards("SelectedKUChanged",this);
    }
}
