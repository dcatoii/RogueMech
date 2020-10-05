using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongbowArrow : Projectile {

    int explosionDamage = 0;

    public override void SetDamage(int value)
    {
        explosionDamage = value;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            Object.Destroy(this.gameObject);
            GameObject.Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        else if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Units") && Source != collision.collider.GetComponentInParent<Mob>())
        {
            if (collision.collider.tag == "Player")
                return;

            MechComponentCollisionDetector frameCollision = collision.collider.GetComponent<MechComponentCollisionDetector>();
            if (frameCollision != null) //we collided with a mech
            {
                frameCollision.component.OnHit(this);
            }
            Object.Destroy(this.gameObject);
            GameObject explosionObj = GameObject.Instantiate(deathEffect, transform.position, Quaternion.identity);
            ExplodingDamageArea explosion = explosionObj.GetComponentInChildren<ExplodingDamageArea>();
            explosion.SetDamage(explosionDamage);
        }
    }
}
