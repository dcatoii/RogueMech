using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMob : Mob {
    public int Health = 1000;
    public Weapon TankGun;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Weapons"))
        {
            Projectile colProjectile = collision.rigidbody.GetComponent<Projectile>();
            //if(colProjectile.Source)
            Health -= colProjectile.Damage;
            if(Health <= 0)
            {
                Die();
            }
        }
    }

    private void FixedUpdate()
    {
        if (Mission.instance.PlayerFrame == null)
            return;

        if ((Mission.instance.PlayerFrame.gameObject.transform.position - TankGun.FirePoint.transform.position).sqrMagnitude < (TankGun.FunctionalRange * TankGun.FunctionalRange))
        {
            TurnTowardsPlayer();
            TankGun.OnFireDown(Mission.instance.PlayerFrame.Core.gameObject.transform.position);
        }
    }

    void TurnTowardsPlayer()
    {
        Vector3 target = Mission.instance.PlayerFrame.gameObject.transform.position;
        target.y = transform.position.y;
        float strength = .5f;

        Quaternion targetRotation = Quaternion.LookRotation(target - transform.position);
        float str = Mathf.Min(strength * Time.deltaTime, 1);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
    }

}
