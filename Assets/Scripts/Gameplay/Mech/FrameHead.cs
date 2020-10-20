using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameHead : FrameComponent {

    public int Weight = 500;
    public int EnergyCost = 200;

    public GameObject CrosshairPrefab;
    public string ReticleStyle = "Crosshair";

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

    public override List<string> GetAttributeNamesForStore()
    {
        List<string> returnList = base.GetAttributeNamesForStore();
        returnList.Add("Energy Cost");
        returnList.Add("Weight");
        returnList.Add("Reticle Style");
        return returnList;
    }

    public override List<string> GetAttributeValuesForStore()
    {
        List<string> returnList = base.GetAttributeValuesForStore();

        returnList.Add(EnergyCost.ToString());
        returnList.Add(Weight.ToString());
        returnList.Add(ReticleStyle);

        return returnList;
    }
}
