using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed = 10;
    public GameObject deathEffect;
    public Mob Source;
    public int Damage = 800;
    Vector3 lastPos;
	// Use this for initialization
	void Start () {
        lastPos = transform.position;
	}
	
    public virtual void SetDamage(int value)
    {
        Damage = value;
    }

	// Update is called once per frame
	protected virtual void FixedUpdate () {

        if (ApplicationContext.Game.IsPaused)
            return;

        // Calculate the desired movement vector
        Vector3 move = transform.forward * Time.fixedDeltaTime * speed;

        //checked if we are hitting the terrain
        HandleCollision(move);

        //update position
        transform.position += move;

    }

    public void SetTarget(Vector3 target)
    {
        transform.LookAt(target);
    }
    /*
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            Die();
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
            Die();
        }       
            
    }*/

    protected void HandleCollision(Vector3 move)
    {
        Vector3 start = transform.position;
        Vector3 end = start + move;
        RaycastHit hitInfo;
        if(Physics.SphereCast(start, transform.localScale.z, move.normalized, out hitInfo, move.magnitude))
        {
            if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Terrain"))
            {
                OnTerrainHit();
            }
            else if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Units"))
            {
                OnMobHit(hitInfo.collider.GetComponent<MechComponentCollisionDetector>());
            }
        }
    }

    protected virtual void OnTerrainHit()
    {
        Die();
    }

    protected virtual void OnMobHit(MechComponentCollisionDetector frameCollision)
    {
        if (frameCollision == null) //we collided with a mech
            return;

        if (frameCollision.component.Mech != Source)
        {
            frameCollision.component.TakeDamage(Damage);
            Die();
        }
    }

    protected virtual void Die()
    {
        Object.Destroy(this.gameObject);
        GameObject deathFX = GameObject.Instantiate(deathEffect, transform.position, Quaternion.identity);
    }
}
