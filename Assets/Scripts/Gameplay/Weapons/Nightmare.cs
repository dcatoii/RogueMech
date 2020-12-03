using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  Nightmare : Weapon
{

    public float EnergyRate = 200f;
    public float ChargeDelay = 1.0f;
    public GameObject ChargeFXPrefab;
    GameObject chargeFX;
    public float  ChargeTime = 0.0f;
    bool isFiring = false;
    bool isCharging = false;
    Beam activeBeam = null;

    public override void OnFireDown(Vector3 target)
    {
        isCharging = true;
        chargeFX = GameObject.Instantiate(ChargeFXPrefab, FirePoint.transform);
    }

    public override void OnFireUp(Vector3 target)
    {
        isFiring = false;
        isCharging = false;

        if (ChargeTime >= ChargeDelay)
        {
            TimeSinceLastFire = 0.0f;
            ChargeTime = 0f;
        }

        if (activeBeam != null)
            GameObject.Destroy(activeBeam.gameObject);

        if (chargeFX != null)
            GameObject.Destroy(chargeFX.gameObject);
    }

    protected override void FixedUpdate()
    {
        if (ApplicationContext.Game.IsPaused)
            return;

        base.FixedUpdate();
        if(isFiring)
        {
            if (Mech.Core.ConsumeEnergy(EnergyRate * Time.fixedDeltaTime) == false)
                OnFireUp(Vector3.zero);
            else
            {
                activeBeam.transform.localPosition = Vector3.zero;
                activeBeam.transform.LookAt(GetTargetWithCameraRay(Camera.main.transform.position + (Camera.main.transform.forward * FunctionalRange)));
            }
        }
        else if (isCharging)
        {
            ChargeTime += Time.fixedDeltaTime;
            if (ChargeTime > ChargeDelay)
            {
                FireProjectile();
                isFiring = true;
            }
        }
        else if (ChargeTime > 0)
        {
            ChargeTime = Mathf.Max(ChargeTime - Time.fixedDeltaTime, 0f);            
        }
    }

    void FireProjectile()
    {
        //Raycast
        Vector3 target = Camera.main.transform.position + (Camera.main.transform.forward * FunctionalRange);

        target = GetTargetWithCameraRay(target);

        //create and fire projectile
        GameObject newProjectileObject = (GameObject.Instantiate(ProjectilePrefab, FirePoint.transform) as GameObject);
        newProjectileObject.GetComponent<Projectile>().Source = GetComponentInParent<MechFrame>();
        newProjectileObject.GetComponent<Projectile>().SetTarget(target);
        newProjectileObject.GetComponent<Projectile>().SetDamage(damage);
        activeBeam = newProjectileObject.GetComponent<Beam>();
        activeBeam.MaxLength = FunctionalRange;
        TimeSinceLastFire = 0.0f;
    }

    public override List<string> GetAttributeNamesForStore()
    {
        List<string> returnList = base.GetAttributeNamesForStore();
        returnList.Add("Power");
        returnList.Add("Charge Up Time");
        returnList.Add("Sustained Beam Energy");
        returnList.Add("Beam Range");
        return returnList;
    }

    public override List<string> GetAttributeValuesForStore()
    {
        List<string> returnList = base.GetAttributeValuesForStore();
        returnList.Add(CalculatePower().ToString());
        returnList.Add(((int)(ChargeDelay * 1000)).ToString());
        returnList.Add(((int)(EnergyRate)).ToString());
        returnList.Add(((int)FunctionalRange).ToString());
        return returnList;
    }

    int CalculatePower()
    {
        float returnPower = damage;
        return (int)(returnPower);
    }
}
