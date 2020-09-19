using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramePart : MonoBehaviour {

    public int Cost;

    public string Manufacturer = "GeneriCon";

    public virtual List<string> GetAttributeNamesForStore()
    {
        return new List<string>(new string[] { "Manufacturer" });
    }

    public virtual List<string> GetAttributeValuesForStore()
    {
        return new List<string>(new string[] { Manufacturer });
    }
}
