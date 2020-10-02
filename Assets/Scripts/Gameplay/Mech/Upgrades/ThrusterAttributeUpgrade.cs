using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterAttributeUpgrade : PartUpgrade {

    public int Weight = 0;
    public int EnergyCost = 0;
    public float ThrusterMaxSpeed = 0.0f;
    public float AccelerationRate = 0.0f;
    public float AscentSpeed = 0.0f;
    public float PowerUsage = 0.0f;
    public float AscentPowerUsage = 0.0f;

    public override void Apply(FramePart Part)
    {
        Thruster Thruster = (Part as Thruster);
        if (Thruster == null)
        {
            Debug.LogError("Core upgrade being applied to non-thruster frame part");
            return;
        }

        Thruster.Weight += Weight;
        Thruster.EnergyCost += EnergyCost;
        Thruster.ThrusterMaxSpeed += ThrusterMaxSpeed;
        Thruster.AccelerationRate += AccelerationRate;
        Thruster.AscentSpeed += AscentSpeed;
        Thruster.PowerUsage += PowerUsage;
        Thruster.AscentPowerUsage += AscentPowerUsage;
    }

    public override void Remove(FramePart Part)
    {
        Thruster Thruster = (Part as Thruster);
        if (Thruster == null)
        {
            Debug.LogError("Core upgrade being applied to non-head frame part");
            return;
        }

        Thruster.Weight -= Weight;
        Thruster.EnergyCost -= EnergyCost;
        Thruster.ThrusterMaxSpeed -= ThrusterMaxSpeed;
        Thruster.AccelerationRate -= AccelerationRate;
        Thruster.AscentSpeed -= AscentSpeed;
        Thruster.PowerUsage -= PowerUsage;
        Thruster.AscentPowerUsage -= AscentPowerUsage;
    }
}
