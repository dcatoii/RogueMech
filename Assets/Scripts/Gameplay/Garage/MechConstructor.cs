using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechConstructor : MonoBehaviour {

    public MechFrame Mech;
    public static MechConstructor instance;

    private void Start()
    {
        instance = this;
    }

    public void ConstructMech(string mechData)
    {
        
    }

	public void SwapHead(FrameHead newHead)
    {
        GameObject newHeadObj = GameObject.Instantiate(newHead.gameObject, Mech.Core.HeadSocket.transform);
        newHeadObj.transform.localPosition = Vector3.zero;
        GameObject.Destroy(Mech.Head.gameObject);
        Mech.Head = newHeadObj.GetComponent<FrameHead>();
        RecalculateDerivedStats();

    }

    public void SwapCore(FrameCore newCore)
    {

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
    }

    void RecalculateDerivedStats()
    {

    }
}
