using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusCollectible : Collectable {

    public int Amount = 5000;

    protected override void Collect(Mob source)
    {
        Mission.instance.BonusCollected(Amount);
        Mission.instance.GoodNotification("Bonus Collected!");
    }
}
