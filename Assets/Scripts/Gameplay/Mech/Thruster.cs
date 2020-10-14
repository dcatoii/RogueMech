using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : FrameAccessory {

    public ParticleSystem FwdThruster;
    public ParticleSystem UpThruster;
    public ParticleSystem LeftThruster;
    public ParticleSystem RightThruster;

    public float ThrusterSpeed;
    public float ThrusterMaxSpeed;
    public float AccelerationRate;
    public float AscentSpeed;

    public int Weight = 500;
    public int EnergyCost = 200;

    /// <summary>
    /// Energy used per second
    /// </summary>
    public float PowerUsage;
    public float AscentPowerUsage;

    public bool Thrusting = false;
    public bool Ascending = false;

    FrameCore Core;
    MechFrame ArmorFrame;

    protected override void Start()
    {
        base.Start();
        Core = GetComponentInParent<FrameCore>();
        ArmorFrame = GetComponentInParent<MechFrame>();
    }

    public void OnThrusterDown()
    {
        if (ThrusterSpeed > 0.0f)
        {
            Ascending = true;
            ArmorFrame.GetComponent<Rigidbody>().useGravity = false;
        }

        Thrusting = true;
    }

    public void OnThrusterUp()
    {
        Ascending = false;
        Thrusting = false;
        ArmorFrame.GetComponent<Rigidbody>().useGravity = true;

        ToggleFwdThruster(false);
        ToggleLeftThruster(false);
        ToggleRightThruster(false);
        ToggleUpThruster(false);
    }

    public void FixedUpdate()
    {
        if (ApplicationContext.Game.IsPaused)
            return;

        if (!Thrusting && ThrusterSpeed > 0.0f)
            ThrusterSpeed = Mathf.Clamp(ThrusterSpeed - Time.fixedDeltaTime * AccelerationRate, 0.0f, ThrusterMaxSpeed);
        else if(Thrusting)
        {
            ThrusterSpeed = Mathf.Clamp(ThrusterSpeed + Time.fixedDeltaTime * AccelerationRate, 0.0f, ThrusterMaxSpeed);
            if(Ascending)
            {
                Vector3 liftVector = new Vector3(0.0f, AscentSpeed, 0.0f);
                ArmorFrame.GetComponent<Rigidbody>().velocity = liftVector;
                Core.ConsumeEnergy(AscentPowerUsage * Time.fixedDeltaTime);
            }
            else
                Core.ConsumeEnergy(PowerUsage * Time.fixedDeltaTime);
        }

    }

    public void ToggleUpThruster(bool isOn)
    {
        ParticleSystem.EmissionModule emission = UpThruster.emission;
        emission.rateOverTime = (isOn ? 25 : 0);
    }

    public void ToggleLeftThruster(bool isOn)
    {
        ParticleSystem.EmissionModule emission = LeftThruster.emission;
        emission.rateOverTime = (isOn ? 25 : 0);
    }

    public void ToggleRightThruster(bool isOn)
    {
        ParticleSystem.EmissionModule emission = RightThruster.emission;
        emission.rateOverTime = (isOn ? 25 : 0);
    }

    public void ToggleFwdThruster(bool isOn)
    {
        ParticleSystem.EmissionModule emission = FwdThruster.emission;
        emission.rateOverTime = (isOn ? 25 : 0);
    }

    public override List<string> GetAttributeNamesForStore()
    {
        List<string> returnList = base.GetAttributeNamesForStore();
        returnList.Add("Energy Cost");
        returnList.Add("Weight");
        returnList.Add("Thrusting Speed");
        returnList.Add("Thrust Energy Drain");
        returnList.Add("Lift Speed");
        returnList.Add("Lift Energy Drain");
        return returnList;
    }

    public override List<string> GetAttributeValuesForStore()
    {
        List<string> returnList = base.GetAttributeValuesForStore();

        returnList.Add(EnergyCost.ToString());
        returnList.Add(Weight.ToString());
        returnList.Add(((int)(ThrusterMaxSpeed * 1000)).ToString());
        returnList.Add(((int)(PowerUsage)).ToString());
        returnList.Add(((int)(AscentSpeed * 1000)).ToString());
        returnList.Add(((int)(AscentPowerUsage)).ToString());

        return returnList;
    }
}
