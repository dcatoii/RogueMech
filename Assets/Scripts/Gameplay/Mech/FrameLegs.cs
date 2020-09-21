using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameLegs : FrameComponent {

    public GameObject CoreSocket;

    public float Speed = 5.0f;
    public float TurnSpeed = 90.0f;

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

    public override List<string> GetAttributeNamesForStore()
    {
        List<string> returnList = base.GetAttributeNamesForStore();
        returnList.Add("Walking Speed");
        returnList.Add("Turning Speed");
        return returnList;
    }

    public override List<string> GetAttributeValuesForStore()
    {
        List<string> returnList = base.GetAttributeValuesForStore();

        returnList.Add(((int)(Speed* 1000)).ToString());
        returnList.Add(((int)(TurnSpeed * 100)).ToString());

        return returnList;
    }
}
