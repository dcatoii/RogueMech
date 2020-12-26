using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongbowArrow : Projectile {

    int explosionDamage = 0;

    public override void SetDamage(int value)
    {
        explosionDamage = value;
    }

    protected override void Die()
    {
        Object.Destroy(this.gameObject);
        GameObject explosionObj = GameObject.Instantiate(deathEffect, transform.position, Quaternion.identity);
        ExplodingDamageArea explosion = explosionObj.GetComponentInChildren<ExplodingDamageArea>();
        explosion.SetDamage(explosionDamage);
        explosion.Source = Source;
    }
}
