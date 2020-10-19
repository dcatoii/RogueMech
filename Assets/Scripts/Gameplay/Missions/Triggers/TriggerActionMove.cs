using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActionMove : TriggeredAction {

    public Transform targetPosition;
    public float moveTime;
    public System.Action callback;

    public override void Activate(Mob source)
    {
        LTDescr descr = LeanTween.move(gameObject, targetPosition.position, moveTime);
        descr.setOnComplete(callback);
    }
}
