using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightmareAttributeUpgrade : WeaponAttributeUpgrade {

    public float EnergyRate = 0f;
    public float ChargeDelay = 0f;
    public float MaxBeamLength = 0f;

    public override void Apply(FramePart Part)
    {
        Nightmare weapon = (Part as Nightmare);
        if (weapon == null)
        {
            Debug.LogError("nightmare upgrade being applied to non-longbow frame part");
            return;
        }


        weapon.Weight += Weight;
        weapon.EnergyCost += EnergyCost;
        weapon.FunctionalRange += FunctionalRange;
        weapon.RefireTime += RefireTime;
        weapon.damage += damage;
        weapon.EnergyRate += EnergyRate;
        weapon.ChargeDelay += ChargeDelay;
        weapon.MaxBeamLength += MaxBeamLength;
    }

    public override void Remove(FramePart Part)
    {
        Nightmare weapon = (Part as Nightmare);
        if (weapon == null)
        {
            Debug.LogError("longbow upgrade being applied to non-longbow frame part");
            return;
        }


        weapon.Weight -= Weight;
        weapon.EnergyCost -= EnergyCost;
        weapon.FunctionalRange -= FunctionalRange;
        weapon.RefireTime -= RefireTime;
        weapon.damage -= damage;
        weapon.EnergyRate -= EnergyRate;
        weapon.ChargeDelay -= ChargeDelay;
        weapon.MaxBeamLength -= MaxBeamLength;

    }
}
