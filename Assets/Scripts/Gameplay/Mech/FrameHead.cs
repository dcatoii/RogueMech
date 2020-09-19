using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameHead : FrameComponent {

    public override void OnHit(Projectile projectile)
    {
        base.OnHit(projectile);
        Mech.BroadcastMessage("HeadDamaged", projectile.Damage);
    }

    protected override void OnPartBroken()
    {
        base.OnPartBroken();
        Mech.BroadcastMessage("HeadBroken");
    }
}
