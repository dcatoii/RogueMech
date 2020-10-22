using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActionPlayFX : TriggeredAction {

    public GameObject FX;
    public Transform Parent;
    public bool UsePopupAsParent = false;
    public bool IsParentIdentity = false;
    public bool IsSourceIdentity = false;

    public override void Activate(Mob source)
    {
        if (UsePopupAsParent)
            Parent = ApplicationContext.PopupRoot.transform;

        GameObject newFX = GameObject.Instantiate(FX, Parent);
        if(IsSourceIdentity)
        {
            newFX.transform.position = transform.position;
        }
        else if (IsParentIdentity && Parent != null)
        {
            newFX.transform.localPosition = Vector3.zero;
        }
    }
}
