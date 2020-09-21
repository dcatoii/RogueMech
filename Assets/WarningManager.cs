using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningManager : MonoBehaviour {

	public int WarningCount { get { return GetComponentsInChildren<WarningBanner>().Length; } }

    public WarningBanner bannerPrefab;

    public void AddWarning(string warningText)
    {
        GameObject warningObj = GameObject.Instantiate(bannerPrefab.gameObject, transform);
        warningObj.GetComponent<WarningBanner>().Text.text = warningText;
    }

    public void ClearWarnings()
    {
        foreach(WarningBanner banner in GetComponentsInChildren<WarningBanner>())
        {
            GameObject.Destroy(banner.gameObject);
        }
    }
}
