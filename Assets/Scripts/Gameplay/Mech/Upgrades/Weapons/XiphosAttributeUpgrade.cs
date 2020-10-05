using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XiphosAttributeUpgrade : WeaponAttributeUpgrade {

    public int MaxCharges = 0;
    public float RechargeCooldown = 0;
    public float BulletRechargeTime = 0;

    public override void Apply(FramePart Part)
    {
        Xiphos weapon = (Part as Xiphos);
        if (weapon == null)
        {
            Debug.LogError("Xiphos upgrade being applied to non-Xiphos frame part");
            return;
        }


        weapon.Weight += Weight;
        weapon.EnergyCost += EnergyCost;
        weapon.FunctionalRange += FunctionalRange;
        weapon.damage += damage;
        weapon.RefireTime += RefireTime;
        weapon.MaxCharges += MaxCharges;
        weapon.RechargeCooldown += RechargeCooldown;
        weapon.BulletRechargeTime += BulletRechargeTime;

    }

    public override void Remove(FramePart Part)
    {
        Xiphos weapon = (Part as Xiphos);
        if (weapon == null)
        {
            Debug.LogError("longbow upgrade being applied to non-longbow frame part");
            return;
        }


        weapon.Weight -= Weight;
        weapon.EnergyCost -= EnergyCost;
        weapon.FunctionalRange -= FunctionalRange;
        weapon.damage -= damage;
        weapon.RefireTime -= RefireTime;
        weapon.MaxCharges -= MaxCharges;
        weapon.RechargeCooldown -= RechargeCooldown;
        weapon.BulletRechargeTime -= BulletRechargeTime;

    }
}
