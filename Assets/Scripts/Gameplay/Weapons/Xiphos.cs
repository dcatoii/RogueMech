using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xiphos : Weapon {

    int Charges = 0;
    bool isFiring;
    float recharge = 0.0f;
    public int MaxCharges = 6;
    public float RechargeCooldown = 1.0f;
    public float BulletRechargeTime = 0.5f;


    protected override void Start()
    {
        base.Start();
        Charges = MaxCharges;
        isFiring = false;
        recharge = 0.0f;
    }

    public override void OnFireDown(Vector3 target)
    {
        if (Charges == 0)
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
                if (Charges == 0)
                    OnFireUp(Vector3.zero);
            }
        }
        else if (Charges < MaxCharges)
        {
            if (TimeSinceLastFire > RechargeCooldown)
            {
                recharge += Time.fixedDeltaTime;
                if(recharge >= BulletRechargeTime)
                {
                    recharge -= BulletRechargeTime;
                    Charges++;
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

        TimeSinceLastFire = 0.0f;
        Charges--;
    }

}
