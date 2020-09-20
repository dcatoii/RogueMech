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

    }

    public void SwapArms (FrameArms newArms)
    {

    }

    void RecalculateDerivedStats()
    {

    }
}
