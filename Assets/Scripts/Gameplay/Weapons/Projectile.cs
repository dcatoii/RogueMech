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
	void FixedUpdate () {

        // Calculate the desired movement vector
        Vector3 move = transform.forward * Time.fixedDeltaTime * speed;
        transform.position += move;

    }

    public void SetTarget(Vector3 target)
    {
        transform.LookAt(target);
    }

    protected virtual void OnCollisionEnter(Collision collision)
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
            GameObject.Instantiate(deathEffect, transform.position, Quaternion.identity);
        }       
            
    }
}
