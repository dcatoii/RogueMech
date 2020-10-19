using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCannon : Weapon {

    public Transform TargetPoint;

    public override void OnFireDown(Vector3 target)
    {
        if(TimeSinceLastFire < RefireTime)
            return;

        GameObject newProjectileObject = (GameObject.Instantiate(ProjectilePrefab, FirePoint.transform.position, Quaternion.identity) as GameObject);
        newProjectileObject.GetComponent<Projectile>().Source = GetComponentInParent<Mob>();
        newProjectileObject.GetComponent<Projectile>().SetTarget(target);
        newProjectileObject.GetComponent<TeleportTriggerAction>().TargetPoint = TargetPoint;
        TimeSinceLastFire = 0.0f;
    }
}
