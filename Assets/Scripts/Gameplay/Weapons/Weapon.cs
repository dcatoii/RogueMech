using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : FrameAccessory {

    public GameObject ProjectilePrefab;
    public GameObject FirePoint;
    public float FunctionalRange = 200.0f;
    public float RefireTime = 0.4f;
    public float TimeSinceLastFire = 0.0f;

    public int Weight = 1000;
    public int EnergyCost = 400;

    // Use this for initialization
    protected override void Start () {
        base.Start();
	}
	
    public virtual void OnFireDown(Vector3 target)
    {
        if (TimeSinceLastFire < RefireTime)
            return;

        GameObject newProjectileObject = (GameObject.Instantiate(ProjectilePrefab, FirePoint.transform.position, Quaternion.identity) as GameObject);
        newProjectileObject.GetComponent<Projectile>().Source = GetComponentInParent<MechFrame>();
        newProjectileObject.GetComponent<Projectile>().SetTarget(target);
        TimeSinceLastFire = 0.0f;
    }

    public virtual void OnFireUp(Vector3 target)
    {

    }

    protected virtual void FixedUpdate()
    {
        TimeSinceLastFire += Time.fixedDeltaTime;
    }

    public override List<string> GetAttributeNamesForStore()
    {
        List<string> returnList = base.GetAttributeNamesForStore();
        returnList.Add("Energy Cost");
        returnList.Add("Weight");
        return returnList;
    }

    public override List<string> GetAttributeValuesForStore()
    {
        List<string> returnList = base.GetAttributeValuesForStore();

        returnList.Add(EnergyCost.ToString());
        returnList.Add(Weight.ToString());

        return returnList;
    }
}
