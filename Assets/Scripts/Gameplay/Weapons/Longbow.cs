using System;
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
        if (TimeSinceLastFire < RefireTime || mech.Core.Energy < BaseEnergyPerShot)
            return;

        isCharging = true;
        chargeLevel = 0;
        energyLevel = 0.0f;

        GenerateProjectile(ProjectilePrefab, damage);
   
        if (mech.Core.ConsumeEnergy(BaseEnergyPerShot) == false)
            OnFireUp(Vector3.zero);


        TimeSinceLastFire = 0.0f;
    }

    public override void OnFireUp(Vector3 target)
    {
        if (!IsCharging)
            return;

        target = GetTargetWithCameraRay(Camera.main.transform.position + (Camera.main.transform.forward * FunctionalRange));

        chargeProjectile.transform.parent = null;
        chargeProjectile.Source = mech;
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
        if (ApplicationContext.Game.IsPaused)
            return;

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
            if (mech.Core.ConsumeEnergy(chargeAmount) == false)
                OnFireUp(Vector3.zero);
        }
    }

    protected void GenerateProjectile(GameObject prefab, int _damage)
    {
        if(chargeProjectile != null)
            GameObject.Destroy(chargeProjectile.gameObject);

        chargeProjectile = GameObject.Instantiate(prefab, FirePoint.transform).GetComponent<Projectile>();
        chargeProjectile.Source = mech;
        chargeProjectile.enabled = false;
        chargeProjectile.GetComponent<Collider>().enabled = false;
        chargeProjectile.transform.localPosition = Vector3.zero;
        chargeProjectile.transform.localRotation = Quaternion.identity;
        chargeProjectile.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        chargeProjectile.SetDamage(_damage);
    }

    public override List<string> GetAttributeNamesForStore()
    {
        List<string> returnList = base.GetAttributeNamesForStore();
        returnList.Add("Power");
        returnList.Add("Energy per Shot");
        returnList.Add("Level 2 Charge");
        returnList.Add("Level 3 Charge");
        returnList.Add("Charge Rate");
        return returnList;
    }

    public override List<string> GetAttributeValuesForStore()
    {
        List<string> returnList = base.GetAttributeValuesForStore();
        returnList.Add(CalculatePower().ToString());
        returnList.Add(BaseEnergyPerShot.ToString());
        returnList.Add(((int)(Level2Charge)).ToString());
        returnList.Add(((int)(Level2Charge + Level3Charge)).ToString());
        returnList.Add(String.Format("{0:0.00}", chargeRate));
        return returnList;
    }

    int CalculatePower()
    {
        float returnPower = 0f;
        float power1 = (float)(damage) / RefireTime;
        float power2 = Level2Damage / Mathf.Max(RefireTime, (Level2Charge / chargeRate));
        float power3 = Level3Damage / Mathf.Max(RefireTime, ((Level3Charge + Level2Charge) / chargeRate));
        returnPower = Mathf.Max(power1, power2, power3);
        return (int)(returnPower);
    }
}
