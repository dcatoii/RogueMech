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

    public static RaycastHit[] SortRaycastArrayByDistance(RaycastHit[] unsorted)
    {
        List<RaycastHit> outArray = new List<RaycastHit>();
        for(int i = 0; i < unsorted.Length; i++)
        {
            for (int j = 0; j < unsorted.Length; j++)
            {
                if (j >= outArray.Count)
                {
                    outArray.Add(unsorted[i]);
                    break;
                }
                else if(unsorted[i].distance < outArray[j].distance)
                {
                    outArray.Insert(j, unsorted[i]);
                    break;
                }

            }
        }
        return outArray.ToArray();
    }
}
