using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TexasChainsaw : Weapon
{

    public float MaxHeat = 5.0f;
    int heat = 0;
    public int HeatPerBullet = 0;
    public float CooldownDelay = 1.0f;
    public float CooldownRate = 2.0f;
    public float OverHeatCooldown = 5.0f;
    bool isFiring = false;
    bool isOverheated = false;
    float bloom;
    public float MaxBloom = 20.0f;
    public float BloomGrowthRate = 15.0f;
    public float BloomCooldown = 1.0f;
    
    public override void OnFireDown(Vector3 target)
    {
        if (isOverheated)
            return;

        isFiring = true;
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
            }
            bloom = Mathf.Min(bloom + (Time.fixedDeltaTime * BloomGrowthRate), MaxBloom);
        }
        else
        {
            if (TimeSinceLastFire > BloomCooldown)
                bloom = Mathf.Min(bloom - (Time.fixedDeltaTime * BloomGrowthRate), 0);
        }
    }

    void FireProjectile()
    {
        //Raycast
        Vector3 target = Camera.main.transform.position + (Camera.main.transform.forward * FunctionalRange);

        //bloom
        target.x += Random.Range(-bloom, bloom);
        target.y += Random.Range(-bloom, bloom);
        target.z += Random.Range(-bloom, bloom);

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
    }
}
