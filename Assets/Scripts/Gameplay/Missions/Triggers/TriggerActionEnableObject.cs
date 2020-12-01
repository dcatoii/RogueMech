using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActionEnableObject : TriggeredAction {

    public List<GameObject> Targets;

    public override void Activate(Mob source)
    {
        base.Activate(source);

        foreach (GameObject obj in Targets)
            obj.SetActive(true);
    }

    public override void Cancel(Mob source)
    {
        base.Cancel(source);
        foreach (GameObject obj in Targets)
            obj.SetActive(false);
    }
}
