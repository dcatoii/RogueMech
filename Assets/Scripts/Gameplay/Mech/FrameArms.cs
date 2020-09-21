using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameArms : FrameComponent {

    public GameObject RightArm_Root;
    public GameObject LeftArm_Root;

    public GameObject RightHand;
    public GameObject LeftHand;

    public int MaxWeight = 2500;
    public int Weight = 1000;
    public int EnergyCost = 400;

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

    public override List<string> GetAttributeNamesForStore()
    {
        List<string> returnList = base.GetAttributeNamesForStore();
        returnList.Add("Energy Cost");
        returnList.Add("Weight");
        returnList.Add("Maximum Weight");
        return returnList;
    }

    public override List<string> GetAttributeValuesForStore()
    {
        List<string> returnList = base.GetAttributeValuesForStore();

        returnList.Add(EnergyCost.ToString());
        returnList.Add(Weight.ToString());
        returnList.Add(MaxWeight.ToString());

        return returnList;
    }
}
