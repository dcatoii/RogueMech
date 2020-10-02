using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadAttributeUpgrade : PartUpgrade {

    public int MaxArmor = 0;
    public int Weight = 0;
    public int EnergyCost = 0;

    public override void Apply(FramePart Part)
    {
        FrameHead Head = (Part as FrameHead);
        if (Head == null)
        {
            Debug.LogError("Core upgrade being applied to non-head frame part");
            return;
        }

        Head.MaxArmor += MaxArmor;
        Head.Weight += Weight;
        Head.EnergyCost += EnergyCost;
    }

    public override void Remove(FramePart Part)
    {
        FrameHead Head = (Part as FrameHead);
        if (Head == null)
        {
            Debug.LogError("Core upgrade being applied to non-head frame part");
            return;
        }

        Head.MaxArmor -= MaxArmor;
        Head.Weight -= Weight;
        Head.EnergyCost -= EnergyCost;
    }
}
