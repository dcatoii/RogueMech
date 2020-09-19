using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramePart : MonoBehaviour {

    public int Cost;

    public virtual List<string> GetAttributeNamesForStore()
    {
        return new List<string>();
    }

    public virtual List<string> GetAttributeValuesForStore()
    {
        return new List<string>();
    }
}
