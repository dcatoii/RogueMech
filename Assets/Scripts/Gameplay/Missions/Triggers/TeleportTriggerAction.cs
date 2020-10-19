using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTriggerAction : TriggeredAction {

    public Transform TargetPoint;
    public bool ForceRotation = true;

    public override void Activate(Mob source)
    {
        source.transform.position = TargetPoint.position;
        if (ForceRotation)
            source.transform.rotation = TargetPoint.rotation;
    }

}
