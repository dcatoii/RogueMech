using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegAttributeUpgrade : PartUpgrade {

    public int MaxArmor = 0;
    public int MaxWeight = 0;
    public int EnergyCost = 0;
    public float Speed = 0.0f;
    public float TurnSpeed = 0.0f;

    public override void Apply(FramePart Part)
    {
        FrameLegs Legs = (Part as FrameLegs);
        if (Legs == null)
        {
            Debug.LogError("Core upgrade being applied to non-legs frame part");
            return;
        }

        Legs.MaxArmor += MaxArmor;
        Legs.MaxWeight += MaxWeight;
        Legs.EnergyCost += EnergyCost;
        Legs.Speed += Speed;
        Legs.TurnSpeed += TurnSpeed;
    }

    public override void Remove(FramePart Part)
    {
        FrameLegs Legs = (Part as FrameLegs);
        if (Legs == null)
        {
            Debug.LogError("Core upgrade being applied to non-arms frame part");
            return;
        }

        Legs.MaxArmor -= MaxArmor;
        Legs.MaxWeight -= MaxWeight;
        Legs.EnergyCost -= EnergyCost;
        Legs.Speed -= Speed;
        Legs.TurnSpeed -= TurnSpeed;
    }
}
