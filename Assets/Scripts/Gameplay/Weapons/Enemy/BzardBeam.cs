using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BzardBeam : Weapon {

    Beam activeBeam = null;

    public override void OnFireDown(Vector3 target)
    {
        if (activeBeam == null)
        {
            GameObject newProjectileObject = (GameObject.Instantiate(ProjectilePrefab, FirePoint.transform.position, FirePoint.transform.rotation) as GameObject);
            activeBeam = newProjectileObject.GetComponent<Beam>();
            newProjectileObject.GetComponent<Projectile>().Source = GetComponentInParent<MechFrame>();
            newProjectileObject.transform.parent = FirePoint.transform;
            activeBeam.MaxLength = FunctionalRange;
            activeBeam.SetDamage(damage);
            
        }
    }

    public override void OnFireUp(Vector3 target)
    {
        if (activeBeam != null)
        {
            TimeSinceLastFire = 0.0f;
            GameObject.Destroy(activeBeam.gameObject);
            activeBeam = null;
        }
    }

    protected override void FixedUpdate()
    {
        if (ApplicationContext.Game.IsPaused || activeBeam != null)
            return;

        TimeSinceLastFire += Time.fixedDeltaTime;
    }

}
