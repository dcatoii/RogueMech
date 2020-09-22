using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramePart : MonoBehaviour {

    public int Cost;
    public Sprite StoreImage;

    public string Manufacturer = "GeneriCon";
    protected MechFrame Mech;

    protected virtual void Start()
    {
        Mech = GetComponentInParent<MechFrame>();
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
