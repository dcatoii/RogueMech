using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameLegs : FrameComponent {

    public GameObject CoreSocket;

    public override void OnHit(Projectile projectile)
    {
        base.OnHit(projectile);
        Mech.BroadcastMessage("LegsDamaged", projectile.Damage);
    }

    protected override void OnPartBroken()
    {
        base.OnPartBroken();
        Mech.BroadcastMessage("LegsBroken");
    }
}
