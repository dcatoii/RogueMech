﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechConstructor : MonoBehaviour {

    public static MechConstructor instance;

    public MechFrame Mech;
    public MechCustomizationData CustomData;
    public MechPartLibrary PartLibrary;

    private void Start()
    {
        instance = this;
        ConstructMech("", null);
    }

    public void ConstructMech(string mechData, Transform location)
    {
        ConstructLegs(CustomData.GetLegs);
        ConstructCore(CustomData.GetCore);
        ConstructArms(CustomData.GetArms);
        ConstructHead(CustomData.GetHead);
        ConstructThruster(CustomData.GetThruster);
        ConstructRightWeapon(CustomData.GetRightWeapon);
    }

	public void SwapHead(FrameHead newHead)
    {
        GameObject newHeadObj = GameObject.Instantiate(newHead.gameObject, Mech.Core.HeadSocket.transform);
        newHeadObj.transform.localPosition = Vector3.zero;
        GameObject.Destroy(Mech.Head.gameObject);
        Mech.Head = newHeadObj.GetComponent<FrameHead>();
        RecalculateDerivedStats();

        CustomData.CustomHead = newHead;
    }

    public void SwapCore(FrameCore newCore)
    {
        GameObject newCoreObj = GameObject.Instantiate(newCore.gameObject, Mech.Legs.CoreSocket.transform);
        newCoreObj.transform.localPosition = Vector3.zero;
        FrameCore newCoreComponent = newCoreObj.GetComponent<FrameCore>();
        
        //update arms
        Mech.Arms.transform.parent = newCoreObj.transform;

        Mech.Arms.RightArm_Root.transform.parent = newCoreComponent.RightArmSocket.transform;
        Mech.Arms.RightArm_Root.transform.localPosition = Vector3.zero;

        Mech.Arms.LeftArm_Root.transform.parent = newCoreComponent.LeftArmSocket.transform;
        Mech.Arms.LeftArm_Root.transform.localPosition = Vector3.zero;

        //update thruster 
        Mech.Core.thruster.transform.parent = newCoreComponent.ThrusterSocket.transform;
        Mech.Core.thruster.transform.localPosition = Vector3.zero;
        newCoreComponent.thruster = Mech.Core.thruster;

        //update head
        Mech.Head.transform.parent = newCoreComponent.HeadSocket.transform;
        Mech.Head.transform.localPosition = Vector3.zero;

        GameObject.Destroy(Mech.Core.gameObject);
        Mech.Core = newCoreComponent;

        RecalculateDerivedStats();
        CustomData.CustomCore = newCore;
    }

    public void SwapLegs (FrameLegs newLegs)
    {
        GameObject newLegsObj = GameObject.Instantiate(newLegs.gameObject, Mech.MechRoot);
        newLegsObj.transform.localPosition = Vector3.zero;
        FrameLegs newLegsComponent = newLegsObj.GetComponent<FrameLegs>();

        Mech.Core.transform.parent = newLegsComponent.CoreSocket.transform;
        Mech.Core.transform.localPosition = Vector3.zero;

        GameObject.Destroy(Mech.Legs.gameObject);
        Mech.Legs = newLegsComponent;

        RecalculateDerivedStats();
        CustomData.CustomLegs = newLegs;
    }

    public void SwapArms (FrameArms newArms)
    {
        GameObject newArmsObj = GameObject.Instantiate(newArms.gameObject, Mech.Core.transform);
        newArmsObj.transform.localPosition = Vector3.zero;
        FrameArms newArmsComponent = newArmsObj.GetComponent<FrameArms>();

        newArmsComponent.RightArm_Root.transform.parent = Mech.Core.RightArmSocket.transform;
        newArmsComponent.RightArm_Root.transform.localPosition = Vector3.zero;

        newArmsComponent.LeftArm_Root.transform.parent = Mech.Core.LeftArmSocket.transform;
        newArmsComponent.LeftArm_Root.transform.localPosition = Vector3.zero;

        Mech.RightHandWeapon.transform.parent = newArmsComponent.RightHand.transform;
        Mech.RightHandWeapon.transform.localPosition = Vector3.zero;

        GameObject.Destroy(Mech.Arms.RightArm_Root.gameObject);
        GameObject.Destroy(Mech.Arms.LeftArm_Root.gameObject);
        GameObject.Destroy(Mech.Arms.gameObject);
        Mech.Arms = newArmsComponent;

        RecalculateDerivedStats();

        CustomData.CustomArms = newArms;
    }

    void RecalculateDerivedStats()
    {

    }

    void ConstructHead(FrameHead newHead)
    {
        GameObject newHeadObj = GameObject.Instantiate(newHead.gameObject, Mech.Core.HeadSocket.transform);
        newHeadObj.transform.localPosition = Vector3.zero;
        Mech.Head = newHeadObj.GetComponent<FrameHead>();
    }

    void ConstructCore(FrameCore newCore)
    {
        GameObject newCoreObj = GameObject.Instantiate(newCore.gameObject, Mech.Legs.CoreSocket.transform);
        newCoreObj.transform.localPosition = Vector3.zero;
        FrameCore newCoreComponent = newCoreObj.GetComponent<FrameCore>();

        Mech.Core = newCoreComponent;
    }

    void ConstructLegs(FrameLegs newLegs)
    {
        GameObject newLegsObj = GameObject.Instantiate(newLegs.gameObject, Mech.MechRoot);
        newLegsObj.transform.localPosition = Vector3.zero;
        FrameLegs newLegsComponent = newLegsObj.GetComponent<FrameLegs>();

        Mech.Legs = newLegsComponent;
    }

    void ConstructArms(FrameArms newArms)
    {
        GameObject newArmsObj = GameObject.Instantiate(newArms.gameObject, Mech.Core.transform);
        newArmsObj.transform.localPosition = Vector3.zero;
        FrameArms newArmsComponent = newArmsObj.GetComponent<FrameArms>();

        newArmsComponent.RightArm_Root.transform.parent = Mech.Core.RightArmSocket.transform;
        newArmsComponent.RightArm_Root.transform.localPosition = Vector3.zero;

        newArmsComponent.LeftArm_Root.transform.parent = Mech.Core.LeftArmSocket.transform;
        newArmsComponent.LeftArm_Root.transform.localPosition = Vector3.zero;
        
        Mech.Arms = newArmsComponent;
    }

    void ConstructThruster(Thruster newThruster)
    {
        GameObject newThrusterObj = GameObject.Instantiate(newThruster.gameObject, Mech.Core.ThrusterSocket.transform);
        newThrusterObj.transform.localPosition = Vector3.zero;
        Mech.Core.thruster = newThrusterObj.GetComponent<Thruster>();
    }

    void ConstructRightWeapon(Weapon newWeapon)
    {
        GameObject newWeaponObj = GameObject.Instantiate(newWeapon.gameObject, Mech.Arms.RightHand.transform);
        newWeaponObj.transform.localPosition = Vector3.zero;
        Mech.RightHandWeapon = newWeaponObj.GetComponent<Weapon>();
    }
}
