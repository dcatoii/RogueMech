using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartPreviewer : MonoBehaviour {

    GameObject CurrentPreview = null;
    public FramePart Part = null;

	public void ChangePreviewObject(GameObject prefab)
    {
        if (CurrentPreview != null)
            GameObject.Destroy(CurrentPreview);

        CurrentPreview = GameObject.Instantiate(prefab, transform);
        CurrentPreview.transform.localPosition = Vector3.zero;
        Part = CurrentPreview.GetComponent<FramePart>();
        UpgradeManager.ApplyUpgrades(Part);
    }
}
