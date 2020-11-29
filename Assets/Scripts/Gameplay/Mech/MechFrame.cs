using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechFrame : Mob {

    public FrameHead Head;
    public FrameArms Arms;
    public FrameLegs Legs;
    public Transform MechRoot;

    public Animator myAnimator;

    public Weapon RightHandWeapon;

    public MechHUD HUD;

    int totalArmWeight = 0;
    int totalEnergyCost = 0;
    int totalLegsWeight = 0;


    public override Transform targetPoint { get { return Core.transform; } }

    // Use this for initialization
    protected override void Start()
    {
        myAnimator = GetComponent<Animator>();
        base.Start();
    }

    /// <summary>
    /// The core takes full damage from every hit. If the core dies, it's GAME OVER MAN!
    /// Could later experiment with the core only taking overflow damage from other parts
    /// </summary>
    /// <param name="amount"></param>
   

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

    
    void ArmsBroken()
    {
        Mission.instance.BadNotification("Arms Destroyed");
    }

    void LegsBroken()
    {
        Mission.instance.BadNotification("Legs Destroyed");
    }

    void HeadBroken()
    {
        Mission.instance.BadNotification("Head Destroyed");
        HUD.gameObject.SetActive(false);
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

    public override void ResetOrientation()
    {
        FrameController controller = GetComponent<FrameController>();
        if (controller != null)
            controller.ResetOrientation();
    }
}
