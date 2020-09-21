using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RogueMech/Part Library")]
public class MechPartLibrary : ScriptableObject {

    public FrameCore[] Cores;
    public FrameLegs[] Legs;
    public FrameArms[] Arms;
    public FrameHead[] Heads;

    public Thruster[] Thrusters;
    public Weapon[] Weapons;
}
