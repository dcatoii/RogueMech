using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingDamageArea : Projectile {

    public float lifeTime = 0.5f;
    float age = 0;
    public Vector3 StartScale = Vector3.zero;
    public Vector3 EndScale = Vector3.zero;
    public bool DestroyOnComplete = false;

    public bool isDamageOverTime = false;
    List<Mob> alreadyHit = new List<Mob>();

    protected override void FixedUpdate()
    {
        if (ApplicationContext.Game.IsPaused)
            return;

        age += Time.fixedDeltaTime;
        gameObject.transform.localScale = Vector3.Lerp(StartScale, EndScale, Mathf.Clamp(age / lifeTime, 0.0f, 1.0f));

        if (age > lifeTime && DestroyOnComplete)
        {
            Object.Destroy(this.gameObject);
            GameObject.Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Units") && Source != collision.collider.GetComponentInParent<Mob>())
        {
            if (collision.collider.tag == "Player")
                return;
            MechComponentCollisionDetector frameCollision = collision.collider.GetComponent<MechComponentCollisionDetector>();
            if (frameCollision != null && frameCollision.component.GetComponentInParent<Mob>() != Source) //we collided with a UNIT
            {
                if (!alreadyHit.Contains(frameCollision.component.GetComponentInParent<Mob>()))
                {
                    frameCollision.component.OnHit(this);
                    alreadyHit.Add(frameCollision.component.GetComponentInParent<Mob>());
                }
            }
        }
    }

}
