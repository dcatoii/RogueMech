using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramePart : MonoBehaviour {

    public int Cost;
    public Sprite StoreImage;
    public string Manufacturer = "GeneriCon";
    public string PartID;
    public bool IsBlueprintUnlockedByDefault = true;
    public List<UpgradeLevel> UpgradeLevels;

    public List<PartUpgrade> AppliedUpgrades = new List<PartUpgrade>();

    protected Mob mech;
    public Mob Mech {  get { return mech; } }
    

    protected virtual void Start()
    {
        mech = GetComponentInParent<Mob>();
    }

    public virtual List<string> GetAttributeNamesForStore()
    {
        return new List<string>(new string[] { "Manufacturer" });
    }

    public virtual List<string> GetAttributeValuesForStore()
    {
        return new List<string>(new string[] { Manufacturer });
    }
}
