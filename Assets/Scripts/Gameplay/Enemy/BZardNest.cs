using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BZardNest : Mob {

    public int Health = 5000;
    public int MaxSpawn = 4;
    public float ActivationRange = 250.0f;
    public float SpawnTime = 5.0f;
    float timeSinceLastSpawn = 0.0f;
    public Transform spawnPoint;
    public BZard prefab;
    public bool Activated = false;

    List<BZard> mySpawn = new List<BZard>();

    protected void FixedUpdate()
    {
        if (ApplicationContext.Game.IsPaused)
            return;

        if(!Activated)
        {
            if ((Mission.instance.PlayerFrame.gameObject.transform.position - transform.position).sqrMagnitude < (ActivationRange * ActivationRange))
            {
                Activated = true;
            }
            else
            {
                return;
            }
        }

        timeSinceLastSpawn += Time.fixedDeltaTime;
        if(timeSinceLastSpawn >= SpawnTime)
        {
            Spawn();
            timeSinceLastSpawn = 0.0f;
        }
    }

    public void Spawn()
    {
        GameObject newObj = (GameObject.Instantiate(prefab.gameObject, spawnPoint.position, spawnPoint.rotation) as GameObject);
        mySpawn.Add(newObj.GetComponent<BZard>());
        newObj.GetComponent<BZard>().Nest = this;
    }

    public void HandleSpawnDeath(BZard bzard)
    {
        mySpawn.Remove(bzard);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Weapons"))
        {
            Activated = true;
            Projectile colProjectile = collision.gameObject.GetComponent<Projectile>();
            Health -= colProjectile.Damage;
            if (Health <= 0)
            {
                Die();
            }
        }
    }
}
