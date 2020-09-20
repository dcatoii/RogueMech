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




    public FrameCore CustomCore;
    public FrameLegs CustomLegs;
    public FrameArms CustomArms;
    public FrameHead CustomHead;

    public Thruster CustomThruster;
    public Weapon CustomRightWeapon;


    public Thruster GetThruster { get { return CustomThruster == null ? DefaultThruster : CustomThruster; } }
    public Weapon GetRightWeapon { get { return CustomRightWeapon == null ? DefaultRightWeapon : CustomRightWeapon; } }

    public class MechData
    {
        public int Core = 0;
        public int Legs = 0;
        public int Arms= 0;
        public int Head= 0;
        public int Thruster = 0;
        public int RightWeapon = 0;
    }

    MechData PlayerMechData = new MechData();

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
    }

    
}

