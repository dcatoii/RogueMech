using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSphere : Mob {
    public int Health = 1000;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Weapons"))
        {
            Projectile colProjectile = collision.gameObject.GetComponent<Projectile>();
            //if(colProjectile.Source)
            Health -= colProjectile.Damage;
            if(Health <= 0)
            {
                Die();
            }
        }
    }

    protected override void Die()
    {
        Mission.instance.BroadcastMessage("OnEnemyDestroyed", MobID, SendMessageOptions.DontRequireReceiver);
        Object.Destroy(this.gameObject);
        GameObject.Instantiate(DeathParticles, transform.position, transform.rotation);
    }

}
