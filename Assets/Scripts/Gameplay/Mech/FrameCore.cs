using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameCore : FrameComponent {


    public GameObject RightArmSocket;
    public GameObject LeftArmSocket;
    public GameObject HeadSocket;
    public GameObject ThrusterSocket;
    public GameObject CameraAnchor;

    public Thruster thruster;
    public float Energy;
    public float EnergyCapacity;
    public float MaxEnergy;
    public float RechargeRate;
    public float RechargeCooldown = 2.0f;
    public float TimeSinceLastEnergyUsed = 0.0f;

    public int Weight = 2000;

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        if (ApplicationContext.Game.IsPaused)
            return;

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
        {
            StopAllEnergySystems();
            return false;
        }
        return true;
    }

    private void StopAllEnergySystems()
    {
        if(thruster != null)
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

    public override List<string> GetAttributeNamesForStore()
    {
        List<string> returnList = base.GetAttributeNamesForStore();

        returnList.Add("Weight");
        returnList.Add("Energy Capacity");
        returnList.Add("Recharge Delay");
        returnList.Add("Recharge Rate");

        return returnList;
    }

    public override List<string> GetAttributeValuesForStore()
    {
        List<string> returnList = base.GetAttributeValuesForStore();


        returnList.Add(Weight.ToString());
        returnList.Add(((int)(EnergyCapacity)).ToString());
        returnList.Add(((int)(RechargeCooldown * 1000)).ToString());
        returnList.Add(((int)(RechargeRate)).ToString());

        return returnList;
    }
}
