using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmAttributeUpgrade : PartUpgrade {

    public int MaxArmor = 0;
    public int MaxWeight = 0;
    public int Weight = 0;
    public int EnergyCost = 0;

    public override void Apply(FramePart Part)
    {
        FrameArms Arms = (Part as FrameArms);
        if (Arms == null)
        {
            Debug.LogError("Core upgrade being applied to non-arms frame part");
            return;
        }

        Arms.MaxArmor += MaxArmor;
        Arms.MaxWeight += MaxWeight;
        Arms.Weight += Weight;
        Arms.EnergyCost += EnergyCost;
    }

    public override void Remove(FramePart Part)
    {
        FrameArms Arms = (Part as FrameArms);
        if (Arms == null)
        {
            Debug.LogError("Core upgrade being applied to non-arms frame part");
            return;
        }

        Arms.MaxArmor -= MaxArmor;
        Arms.MaxWeight -= MaxWeight;
        Arms.Weight -= Weight;
        Arms.EnergyCost -= EnergyCost;
    }
}
