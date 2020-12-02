using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : TriggeredAction {

    public override void Activate(Mob source)
    {
        Collect(source);
    }

    protected virtual void Collect(Mob source)
    {

    }
}
