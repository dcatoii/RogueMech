using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameArms : FrameComponent {

    public override void OnHit(Projectile projectile)
    {
        base.OnHit(projectile);
        Mech.BroadcastMessage("ArmsDamaged", projectile.Damage);
    }

    protected override void OnPartBroken()
    {
        base.OnPartBroken();
        Mech.BroadcastMessage("ArmsBroken");
    }
}
