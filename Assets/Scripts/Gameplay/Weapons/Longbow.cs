using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Longbow : Weapon {

    bool isCharging = false;
    public bool IsCharging { get { return isCharging; } }
    int chargeLevel = 0;
    public int ChargeLevel { get { return chargeLevel; } }

    public int BaseEnergyPerShot = 100;
    public float chargeRate = 50;
    public int Level2Damage = 0;
    public int Level3Damage = 0;
    public float Level2Charge = 50;
    public float Level3Charge = 50;
    public GameObject ProjectilePrefabLevel2;
    public GameObject ProjectilePrefabLevel3;
    float energyLevel = 0.0f;
    public float EnergyLevel { get { return energyLevel; } }

    Projectile chargeProjectile = null;

    public override void OnFireDown(Vector3 target)
    {
        if (TimeSinceLastFire < RefireTime || Mech.Core.Energy < BaseEnergyPerShot)
            return;

        isCharging = true;
        chargeLevel = 0;
        energyLevel = 0.0f;

        GenerateProjectile(ProjectilePrefab, damage);
   
        if (Mech.Core.ConsumeEnergy(BaseEnergyPerShot) == false)
            OnFireUp(Vector3.zero);


        TimeSinceLastFire = 0.0f;
    }

    public override void OnFireUp(Vector3 target)
    {
        if (!IsCharging)
            return;

        target = Camera.main.transform.position + (Camera.main.transform.forward * Mech.RightHandWeapon.FunctionalRange);
        RaycastHit CameraRayInfo = new RaycastHit();

        RogueMechUtils.SetChildLayerRecursively(Mech.gameObject, LayerMask.NameToLayer("Ignore Raycast"));
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out CameraRayInfo, Mech.RightHandWeapon.FunctionalRange, LayerMask.GetMask(new string[] { "Terrain", "Units" })))
        {
            target = CameraRayInfo.point;
        }
        RogueMechUtils.SetChildLayerRecursively(Mech.gameObject, LayerMask.NameToLayer("Units"));

        chargeProjectile.transform.parent = null;
        chargeProjectile.Source = Mech;
        chargeProjectile.SetTarget(target);
        chargeProjectile.enabled = true;
        chargeProjectile.GetComponent<Collider>().enabled = true;
        chargeProjectile.GetComponent<Rigidbody>().useGravity = true;
        chargeProjectile.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll ^ RigidbodyConstraints.FreezePositionY;
        RogueMechUtils.SetChildLayerRecursively(chargeProjectile.gameObject, LayerMask.NameToLayer("Weapons"));
        chargeProjectile = null;

        chargeLevel = 0;
        isCharging = false;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if(isCharging && chargeLevel < 2)
        {
            float chargeAmount = chargeRate * Time.fixedDeltaTime;
            energyLevel += chargeAmount;
            if (chargeLevel == 0 && energyLevel >= Level2Charge)
            {

                GenerateProjectile(ProjectilePrefabLevel2, Level2Damage);

                Debug.Log("Charge level 2");
                chargeLevel = 1;
                energyLevel -= Level2Charge;
            }
            if (chargeLevel == 1 && energyLevel >= Level3Charge)
            {
                GenerateProjectile(ProjectilePrefabLevel3, Level3Damage);

                Debug.Log("Charge level 3");
                chargeLevel = 2;
                energyLevel -= Level3Charge;
            }
            if (Mech.Core.ConsumeEnergy(chargeAmount) == false)
                OnFireUp(Vector3.zero);
        }
    }

    protected void GenerateProjectile(GameObject prefab, int _damage)
    {
        if(chargeProjectile != null)
            GameObject.Destroy(chargeProjectile.gameObject);

        chargeProjectile = GameObject.Instantiate(prefab, FirePoint.transform).GetComponent<Projectile>();
        chargeProjectile.Source = Mech;
        chargeProjectile.enabled = false;
        chargeProjectile.GetComponent<Collider>().enabled = false;
        chargeProjectile.transform.localPosition = Vector3.zero;
        chargeProjectile.transform.localRotation = Quaternion.identity;
        chargeProjectile.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        chargeProjectile.SetDamage(_damage);
    }
}
