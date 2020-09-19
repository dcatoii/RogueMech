using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechComponentCollisionDetector : MonoBehaviour {

    public FrameComponent component;

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (!component.IsInvulnerable && collision.collider.gameObject.layer == LayerMask.NameToLayer("Weapons"))
    //    {
    //        Projectile colProjectile = collision.rigidbody.GetComponent<Projectile>();
    //        if (colProjectile.Source != GetComponentInParent<Mob>())
    //        {
    //            component.OnHit(colProjectile);
    //        }
    //    }
    //}
}
