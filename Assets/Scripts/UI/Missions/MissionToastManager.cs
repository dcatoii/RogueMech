using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionToastManager : MonoBehaviour {

    public MissionToast BadToastPrefab;
    public MissionToast GoodToastPrefab;
    public MissionToast NeutralToastPrefab;

    public void ShowBadToast(string warningText)
    {
        GameObject warningObj = GameObject.Instantiate(BadToastPrefab.gameObject, transform);
        warningObj.GetComponent<MissionToast>().Text.text = warningText;
    }
    public void ShowGoodToast(string warningText)
    {
        GameObject warningObj = GameObject.Instantiate(GoodToastPrefab.gameObject, transform);
        warningObj.GetComponent<MissionToast>().Text.text = warningText;
    }
    public void ShowNeutralToast(string warningText)
    {
        GameObject warningObj = GameObject.Instantiate(NeutralToastPrefab.gameObject, transform);
        warningObj.GetComponent<MissionToast>().Text.text = warningText;
    }

    public void ClearToasts()
    {
        foreach (WarningBanner banner in GetComponentsInChildren<WarningBanner>())
        {
            GameObject.Destroy(banner.gameObject);
        }
    }
}
