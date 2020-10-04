using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttributeUpgrade : PartUpgrade {

    public int Weight = 0;
    public int EnergyCost = 0;
    public int damage = 0;
    public float FunctionalRange = 0.0f;
    public float RefireTime = 0.0f;


    public override void Apply(FramePart Part)
    {
        Weapon weapon = (Part as Weapon);
        if (weapon == null)
        {
            Debug.LogError("Core upgrade being applied to non-weapon frame part");
            return;
        }

        weapon.Weight += Weight;
        weapon.EnergyCost += EnergyCost;
        weapon.FunctionalRange += FunctionalRange;
        weapon.RefireTime += RefireTime;
    }

    public override void Remove(FramePart Part)
    {
        Weapon weapon = (Part as Weapon);
        if (weapon == null)
        {
            Debug.LogError("Core upgrade being applied to non-weapon frame part");
            return;
        }

        weapon.Weight -= Weight;
        weapon.EnergyCost -= EnergyCost;
        weapon.FunctionalRange -= FunctionalRange;
        weapon.RefireTime -= RefireTime;
    }
}
