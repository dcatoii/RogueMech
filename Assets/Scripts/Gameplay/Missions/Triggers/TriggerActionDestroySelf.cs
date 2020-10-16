﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActionDestroySelf : TriggeredAction {

    public GameObject DeathEffect;

    public override void Activate()
    {
        base.Activate();
        if (DeathEffect != null)
        {
            GameObject FXObject = GameObject.Instantiate(DeathEffect);
            FXObject.transform.position = transform.position;
            FXObject.transform.rotation = transform.rotation;
        }
        GameObject.Destroy(this.gameObject);

    }
}
