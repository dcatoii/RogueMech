using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : FrameAccessory {

    public GameObject ProjectilePrefab;
    public GameObject FirePoint;
    public float FunctionalRange = 200.0f;
    public float RefireTime = 0.4f;
    public float TimeSinceLastFire = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
    public void OnFireStart(Vector3 target)
    {
        if (TimeSinceLastFire < RefireTime)
            return;

        GameObject newProjectileObject = (GameObject.Instantiate(ProjectilePrefab, FirePoint.transform.position, Quaternion.identity) as GameObject);
        newProjectileObject.GetComponent<Projectile>().Source = GetComponentInParent<MechFrame>();
        newProjectileObject.GetComponent<Projectile>().SetTarget(target);
        TimeSinceLastFire = 0.0f;
    }

    private void FixedUpdate()
    {
        TimeSinceLastFire += Time.fixedDeltaTime;
    }
}
