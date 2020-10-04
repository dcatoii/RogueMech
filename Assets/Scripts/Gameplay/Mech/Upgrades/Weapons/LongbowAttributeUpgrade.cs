using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongbowAttributeUpgrade : WeaponAttributeUpgrade {

    public int BaseEnergyPerShot = 0;
    public float chargeRate = 0;
    public int Level2Damage = 0;
    public int Level3Damage = 0;
    public float Level2Charge = 0;
    public float Level3Charge = 0;

    public override void Apply(FramePart Part)
    {
        Longbow weapon = (Part as Longbow);
        if (weapon == null)
        {
            Debug.LogError("longbow upgrade being applied to non-longbow frame part");
            return;
        }


        weapon.Weight += Weight;
        weapon.EnergyCost += EnergyCost;
        weapon.FunctionalRange += FunctionalRange;
        weapon.RefireTime += RefireTime;
        weapon.damage += damage;
        weapon.Level2Damage += Level2Damage;
        weapon.Level3Damage += Level3Damage;
        weapon.BaseEnergyPerShot += BaseEnergyPerShot;
        weapon.chargeRate += chargeRate;
        weapon.Level2Charge += Level2Charge;
        weapon.Level3Charge += Level3Charge;

    }

    public override void Remove(FramePart Part)
    {
        Longbow weapon = (Part as Longbow);
        if (weapon == null)
        {
            Debug.LogError("longbow upgrade being applied to non-longbow frame part");
            return;
        }


        weapon.Weight -= Weight;
        weapon.EnergyCost -= EnergyCost;
        weapon.FunctionalRange -= FunctionalRange;
        weapon.RefireTime -= RefireTime;
        weapon.Level2Damage -= Level2Damage;
        weapon.Level3Damage -= Level3Damage;
        weapon.BaseEnergyPerShot -= BaseEnergyPerShot;
        weapon.chargeRate -= chargeRate;
        weapon.Level2Charge -= Level2Charge;
        weapon.Level3Charge -= Level3Charge;

    }
}
