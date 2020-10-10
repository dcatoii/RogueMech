using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xiphos : Weapon {

    int charges = 0;
    public int Charges {  get { return charges; } }
    bool isFiring;
    float recharge = 0.0f;
    public float Recharge { get { return recharge; } }
    public int MaxCharges = 6;
    public float RechargeCooldown = 1.0f;
    public float BulletRechargeTime = 0.5f;


    protected override void Start()
    {
        base.Start();
        charges = MaxCharges;
        isFiring = false;
        recharge = 0.0f;
    }

    public override void OnFireDown(Vector3 target)
    {
        if (charges == 0)
            return;



        isFiring = true;
        if (TimeSinceLastFire > RefireTime)
            FireProjectile();
    }

    public override void OnFireUp(Vector3 target)
    {
        isFiring = false;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (isFiring)
        {
            if (TimeSinceLastFire > RefireTime)
            {
                FireProjectile();
                if (charges == 0)
                    OnFireUp(Vector3.zero);
            }
        }
        else if (charges < MaxCharges)
        {
            if (TimeSinceLastFire > RechargeCooldown)
            {
                recharge += Time.fixedDeltaTime;
                if(recharge >= BulletRechargeTime)
                {
                    recharge -= BulletRechargeTime;
                    charges++;
                }
            }
        }
    }

    void FireProjectile()
    {
        //Raycast
        Vector3 target = Camera.main.transform.position + (Camera.main.transform.forward * FunctionalRange);
        
        RaycastHit CameraRayInfo = new RaycastHit();

        RogueMechUtils.SetChildLayerRecursively(Mech.gameObject, LayerMask.NameToLayer("Ignore Raycast"));
        if (Physics.Raycast(Camera.main.transform.position, (target - Camera.main.transform.position).normalized, out CameraRayInfo, Mech.RightHandWeapon.FunctionalRange, LayerMask.GetMask(new string[] { "Terrain", "Units" })))
        {
            target = CameraRayInfo.point;
        }
        RogueMechUtils.SetChildLayerRecursively(Mech.gameObject, LayerMask.NameToLayer("Units"));

        //create and fire projectile
        GameObject newProjectileObject = (GameObject.Instantiate(ProjectilePrefab, FirePoint.transform.position, Quaternion.identity) as GameObject);
        newProjectileObject.GetComponent<Projectile>().Source = GetComponentInParent<MechFrame>();
        newProjectileObject.GetComponent<Projectile>().SetTarget(target);
        newProjectileObject.GetComponent<Projectile>().SetDamage(damage);

        TimeSinceLastFire = 0.0f;
        recharge = 0.0f;
        charges--;
    }

    public override List<string> GetAttributeNamesForStore()
    {
        List<string> returnList = base.GetAttributeNamesForStore();
        returnList.Add("Power");
        returnList.Add("Clip Size");
        returnList.Add("Recharge Delay");
        returnList.Add("Bullet Charge Time");
        return returnList;
    }

    public override List<string> GetAttributeValuesForStore()
    {
        List<string> returnList = base.GetAttributeValuesForStore();
        returnList.Add(CalculatePower().ToString());
        returnList.Add(MaxCharges.ToString());
        returnList.Add(((int)(RechargeCooldown * 1000)).ToString());
        returnList.Add(((int)(BulletRechargeTime* 1000)).ToString());
        return returnList;
    }

    int CalculatePower()
    {
        float returnPower = 0f;
        float fullReloadtime = RechargeCooldown + (BulletRechargeTime * MaxCharges);
        float unloadTime = MaxCharges * RefireTime;
        float clipDamage = MaxCharges * damage;
        returnPower = clipDamage / (fullReloadtime + unloadTime);
        return (int)(returnPower);
    }
}
