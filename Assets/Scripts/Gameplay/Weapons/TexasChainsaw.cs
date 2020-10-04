using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TexasChainsaw : Weapon
{

    public float MaxHeat = 5.0f;
    float heat = 0;
    public float CurrentHeat { get { return heat; } }
    public float HeatPerBullet = 0.1f;
    public float CooldownDelay = 1.0f;
    public float CooldownRate = 2.0f;
    public float OverHeatCooldown = 5.0f;
    bool isFiring = false;
    bool isOverheated = false;
    public bool IsOverheated {  get { return isOverheated; } }
    float bloom;
    public float MaxBloom = 20.0f;
    public float BloomGrowthRate = 15.0f;
    public float BloomCooldown = 1.0f;
    public float MinHeatMultiplier = 1.0f;
    public float MaxHeatMultiplier = 1.0f;

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
                heat += HeatPerBullet;
                if(heat > MaxHeat)
                {
                    isOverheated = true;
                    OnFireUp(Vector3.zero);
                }
            }
            bloom = Mathf.Min(bloom + (Time.fixedDeltaTime * BloomGrowthRate), MaxBloom);
        }
        else
        {
            if (TimeSinceLastFire > BloomCooldown)
                bloom = Mathf.Max(bloom - (Time.fixedDeltaTime * BloomGrowthRate), 0.0f);

            if (TimeSinceLastFire > CooldownDelay)
                heat = Mathf.Max(heat - (CooldownRate * Time.fixedDeltaTime), 0.0f);

            if (isOverheated && TimeSinceLastFire > OverHeatCooldown)
            {
                //TODO: OVERHEAT WARNING
                isOverheated = false;
            }
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
        newProjectileObject.GetComponent<Projectile>().SetDamage((int)(damage * LeanTween.easeInQuad(MinHeatMultiplier, MaxHeatMultiplier, heat/MaxHeat)));

        TimeSinceLastFire = 0.0f;
    }
}
