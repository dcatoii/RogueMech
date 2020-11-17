using System;
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

    public static string intToCurrency(int value)
    {
        string CashString = "$" + String.Format("{0:n0}", value);
        return CashString;
    }

    public static string GetTimeString(int totalSeconds)
    {
        int sec = totalSeconds % 60;
        int min = totalSeconds / 60;
        int hr = totalSeconds / 3600;
        string timeString = (hr > 0) ? hr.ToString() + " : " : "";
        timeString += ((min > 9) ? "" : "0") + min.ToString() + " : ";
        timeString += ((sec > 9) ? "" : "0") + sec.ToString();
        return timeString;
    }

    public static string GetTimeString(float totalSeconds)
    {
        return GetTimeString((int)totalSeconds);
    }
}
