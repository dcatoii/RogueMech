using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameArms : FrameComponent {

    public GameObject RightArm_Root;
    public GameObject LeftArm_Root;

    public GameObject RightHand;
    public GameObject LeftHand;

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
