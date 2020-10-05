using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechFrame : Mob {

    public FrameCore Core;
    public FrameHead Head;
    public FrameArms Arms;
    public FrameLegs Legs;
    public Transform MechRoot;

    public Animator myAnimator;

    public Weapon RightHandWeapon;

    int totalArmWeight = 0;
    int totalEnergyCost = 0;
    int totalLegsWeight = 0;

    // Use this for initialization
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    /// <summary>
    /// The core takes full damage from every hit. If the core dies, it's GAME OVER MAN!
    /// Could later experiment with the core only taking overflow damage from other parts
    /// </summary>
    /// <param name="amount"></param>
    void CoreDamaged(int amount)
    {
        Core.TakeDamage(amount);
    }

    void LegsDamaged(int amount)
    {
        Legs.TakeDamage(amount);
        CoreDamaged(amount);
    }

    void HeadDamaged(int amount)
    {
        Head.TakeDamage(amount);
        CoreDamaged(amount);
    }

    void ArmsDamaged(int amount)
    {
        Arms.TakeDamage(amount);
        CoreDamaged(amount);
    }

    void CoreBroken()
    {
        Die();
    }

    void ArmsBroken()
    {

    }

    void LegsBroken()
    {

    }

    void HeadBroken()
    {

    }

    protected override void Die()
    {
        Mission.instance.PlayerFrame = null;
        Camera.main.transform.parent = null;
        Mission.instance.EndMission(false);
        //Cursor.lockState = CursorLockMode.None;
        base.Die();
    }

    public List<string> CalculateDerivedStats()
    {
        List<string> returnList = new List<string>();

        UpdateEnergyCost();
        UpdateLegWeight();
        UpdateArmWeight();


        if(Core.MaxEnergy < 0)
            returnList.Add("Insufficient Energy");

        if (totalLegsWeight > Legs.MaxWeight)
            returnList.Add("Legs Overweight");

        if(totalArmWeight > Arms.MaxWeight)
            returnList.Add("Arms Overweight");


        Core.ArmorPoints = Core.MaxArmor;
        Legs.ArmorPoints = Legs.MaxArmor;
        Arms.ArmorPoints = Arms.MaxArmor;
        Head.ArmorPoints = Head.MaxArmor;

        return returnList;
    }

    private void UpdateArmWeight()
    {
        totalArmWeight = 0;
        totalArmWeight += RightHandWeapon.Weight;
    }

    private void UpdateLegWeight()
    {
        
        totalLegsWeight = 0;
        totalLegsWeight += Core.Weight;
        totalLegsWeight += Arms.Weight;
        totalLegsWeight += Head.Weight;
        totalLegsWeight += Core.thruster.Weight;
        totalLegsWeight += RightHandWeapon.Weight;
    }

    private void UpdateEnergyCost()
    {
        totalEnergyCost = 0;
        totalEnergyCost += Legs.EnergyCost;
        totalEnergyCost += Arms.EnergyCost;
        totalEnergyCost += Head.EnergyCost;
        totalEnergyCost += Core.thruster.EnergyCost;
        totalEnergyCost += RightHandWeapon.EnergyCost;
        Core.Energy = Core.MaxEnergy = Core.EnergyCapacity - totalEnergyCost;
    }
}
