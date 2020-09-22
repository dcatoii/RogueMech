using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueMechUtils {

    public static void SetChildLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;
        foreach (Transform child in obj.transform)
        {
            SetChildLayerRecursively(child.gameObject, newLayer);
        }
    }
}
