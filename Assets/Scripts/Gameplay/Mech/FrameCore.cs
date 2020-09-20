using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameCore : FrameComponent {


    public GameObject RightArmSocket;
    public GameObject LeftArmSocket;
    public GameObject HeadSocket;
    public GameObject ThrusterSocket;

    public Thruster thruster;
    public float Energy;
    public float MaxEnergy;
    public float RechargeRate;
    public float RechargeCooldown = 2.0f;
    public float TimeSinceLastEnergyUsed = 0.0f;

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (TimeSinceLastEnergyUsed >= RechargeCooldown)
            Energy = Mathf.Clamp(Energy + RechargeRate * Time.fixedDeltaTime, float.MinValue, MaxEnergy);
        else
            TimeSinceLastEnergyUsed += Time.fixedDeltaTime;
    }

    public bool ConsumeEnergy(float amount)
    {
        TimeSinceLastEnergyUsed = 0.0f;
        Energy -= amount;
        if (Energy <= 0.0f)
            StopAllEnergySystems();
        return true;
    }

    private void StopAllEnergySystems()
    {
        thruster.OnThrusterUp();
    }

    public override void OnHit(Projectile projectile)
    {
        base.OnHit(projectile);
        Mech.BroadcastMessage("CoreDamaged", projectile.Damage);
    }

    protected override void OnPartBroken()
    {
        base.OnPartBroken();
        Mech.BroadcastMessage("CoreBroken");
    }
}
