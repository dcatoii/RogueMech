using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreAttributeUpgrade : PartUpgrade {

    public int MaxArmor = 0;
    public float EnergyCapacity = 0.0f;
    public float RechargeRate= 0.0f;
    public float RechargeCooldown = 0.0f;
    public int Weight = 0;

    public override void Apply(FramePart Part)
    {
        FrameCore Core = (Part as FrameCore);
        if(Core == null)
        {
            Debug.LogError("Core upgrade being applied to non-core frame part");
            return;
        }

        Core.MaxArmor += MaxArmor;
        Core.EnergyCapacity += EnergyCapacity;
        Core.RechargeRate += RechargeRate;
        Core.RechargeCooldown += RechargeCooldown;
        Core.Weight += Weight;
    }

    public override void Remove(FramePart Part)
    {
        FrameCore Core = (Part as FrameCore);
        if (Core == null)
        {
            Debug.LogError("Core upgrade being applied to non-core frame part");
            return;
        }

        Core.MaxArmor -= MaxArmor;
        Core.EnergyCapacity -= EnergyCapacity;
        Core.RechargeRate -= RechargeRate;
        Core.RechargeCooldown -= RechargeCooldown;
        Core.Weight -= Weight;
    }
}
