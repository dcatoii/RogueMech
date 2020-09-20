using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RogueMech/Customization Data")]
public class MechCustomizationData : ScriptableObject {

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
}
