using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TexasChainsawAttributeUpgrade : WeaponAttributeUpgrade {

    public float MaxHeat = 0.0f;
    public float HeatPerBullet = 0.0f;
    public float CooldownDelay = 0.0f;
    public float CooldownRate = 0.0f;
    public float OverHeatCooldown = 0.0f;
    public float MaxBloom = 0.0f;
    public float BloomGrowthRate = 0.0f;
    public float BloomCooldown = 0.0f;
    public float MinHeatMultiplier = 0.0f;
    public float MaxHeatMultiplier = 0.0f;

    public override void Apply(FramePart Part)
    {
        TexasChainsaw weapon = (Part as TexasChainsaw);
        if (weapon == null)
        {
            Debug.LogError("TexasChainsaw upgrade being applied to non-TexasChainsaw frame part");
            return;
        }


        weapon.Weight += Weight;
        weapon.EnergyCost += EnergyCost;
        weapon.FunctionalRange += FunctionalRange;
        weapon.damage += damage;
        weapon.RefireTime += RefireTime;
        weapon.MaxHeat += MaxHeat;
        weapon.HeatPerBullet += HeatPerBullet;
        weapon.CooldownDelay += CooldownDelay;
        weapon.CooldownRate += CooldownRate;
        weapon.OverHeatCooldown += OverHeatCooldown;
        weapon.MaxBloom += MaxBloom;
        weapon.BloomGrowthRate += BloomGrowthRate;
        weapon.BloomCooldown += BloomCooldown;
        weapon.MinHeatMultiplier += MinHeatMultiplier;
        weapon.MaxHeatMultiplier += MaxHeatMultiplier;

    }

    public override void Remove(FramePart Part)
    {
        TexasChainsaw weapon = (Part as TexasChainsaw);
        if (weapon == null)
        {
            Debug.LogError("TexasChainsaw upgrade being applied to non-TexasChainsaw frame part");
            return;
        }


        weapon.Weight -= Weight;
        weapon.EnergyCost -= EnergyCost;
        weapon.FunctionalRange -= FunctionalRange;
        weapon.damage -= damage;
        weapon.RefireTime -= RefireTime;
        weapon.MaxHeat -= MaxHeat;
        weapon.HeatPerBullet -= HeatPerBullet;
        weapon.CooldownDelay -= CooldownDelay;
        weapon.CooldownRate -= CooldownRate;
        weapon.OverHeatCooldown -= OverHeatCooldown;
        weapon.MaxBloom -= MaxBloom;
        weapon.BloomGrowthRate -= BloomGrowthRate;
        weapon.BloomCooldown -= BloomCooldown;
        weapon.MinHeatMultiplier -= MinHeatMultiplier;
        weapon.MaxHeatMultiplier -= MaxHeatMultiplier;

    }
}
