using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RogueMech/Customization Data")]
public class MechCustomizationData : ScriptableObject {

    public MechPartLibrary Library;

    public FrameCore DefaultCore;
    public FrameLegs DefaultLegs;
    public FrameArms DefaultArms;
    public FrameHead DefaultHead;

    public Thruster DefaultThruster;
    public Weapon DefaultRightWeapon;




    public FrameCore CustomCore { get { return Library.Cores[PlayerMechData.Core]; } }
    public FrameLegs CustomLegs { get { return Library.Legs[PlayerMechData.Legs]; } }
    public FrameArms CustomArms { get { return Library.Arms[PlayerMechData.Arms]; } }
    public FrameHead CustomHead { get { return Library.Heads[PlayerMechData.Head]; } }

    public Thruster CustomThruster { get { return Library.Thrusters[PlayerMechData.Thruster]; } }
    public Weapon CustomRightWeapon { get { return Library.Weapons[PlayerMechData.RightWeapon]; } }


    public Thruster GetThruster { get { return CustomThruster == null ? DefaultThruster : CustomThruster; } }
    public Weapon GetRightWeapon { get { return CustomRightWeapon == null ? DefaultRightWeapon : CustomRightWeapon; } }

    public MechData PlayerMechData = new MechData();

    public FrameCore GetCore { get { return CustomCore == null ? DefaultCore : CustomCore; } }
    public FrameLegs GetLegs { get { return CustomLegs == null ? DefaultLegs : CustomLegs; } }
    public FrameArms GetArms { get { return CustomArms == null ? DefaultArms : CustomArms; } }
    public FrameHead GetHead { get { return CustomHead == null ? DefaultHead : CustomHead; } }

    public void SaveMechData()
    {
        PlayerData.CustomMechData = JsonConvert.SerializeObject(PlayerMechData);
    }

    public void LoadMechData()
    {
        PlayerMechData = JsonConvert.DeserializeObject<MechData>(PlayerData.CustomMechData);
        if (PlayerMechData == null)
            PlayerMechData = new MechData();
    }

    public FramePart GetPartByType(InventoryCatalogue.PartCategory category)
    {
        switch (category)
        {
            case (InventoryCatalogue.PartCategory.Legs):
                {
                    return GetLegs;
                }
            case (InventoryCatalogue.PartCategory.Core):
                {
                    return GetCore;
                }
            case (InventoryCatalogue.PartCategory.Arms):
                {
                    return GetArms;
                }
            case (InventoryCatalogue.PartCategory.Head):
                {
                    return GetHead;
                }
            case (InventoryCatalogue.PartCategory.Thruster):
                {
                    return GetThruster;
                }
            case (InventoryCatalogue.PartCategory.Weapon_Right):
                {
                    return GetRightWeapon;
                }
        }
        return null;
    }

    
}

public class MechData
{
    public int Core = 0;
    public int Legs = 0;
    public int Arms = 0;
    public int Head = 0;
    public int Thruster = 0;
    public int RightWeapon = 0;
}

