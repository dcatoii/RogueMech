using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingDamageArea : Projectile {

    public float lifeTime = 0.5f;
    float age = 0;
    public Vector3 StartScale = Vector3.zero;
    public Vector3 EndScale = Vector3.zero;

    public bool isDamageOverTime = false;

    private void FixedUpdate()
    {
        if (ApplicationContext.Game.IsPaused)
            return;

        age += Time.fixedDeltaTime;
        gameObject.transform.localScale = Vector3.Lerp(StartScale, EndScale, Mathf.Clamp(age / lifeTime, 0.0f, 1.0f));
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        //do nothing
    }

}
