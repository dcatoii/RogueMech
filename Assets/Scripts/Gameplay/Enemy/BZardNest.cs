using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BZardNest : Mob {

    public int MaxSpawn = 4;
    public float ActivationRange = 250.0f;
    public float SpawnTime = 5.0f;
    float timeSinceLastSpawn = 0.0f;
    public Transform spawnPoint;
    public BZard prefab;
    public bool Activated = false;
    public bool CountSpawnTimeWhileMaxSpawns = false;

    List<BZard> mySpawn = new List<BZard>();

    protected void FixedUpdate()
    {
        if (ApplicationContext.Game.IsPaused)
            return;

        if (!Activated)
        {
            Vector3 toPlayer = Mission.instance.PlayerFrame.Core.gameObject.transform.position - spawnPoint.position;
            if (toPlayer.sqrMagnitude < (ActivationRange * ActivationRange))
            {
                //Line-of-Sight check
                if (Physics.Raycast(spawnPoint.position, toPlayer.normalized, toPlayer.magnitude, LayerMask.GetMask(new string[] { "Terrain" }))) 
                {
                    Debug.DrawLine(spawnPoint.position, Mission.instance.PlayerFrame.transform.position, Color.red);
                }
                else
                {
                    Debug.DrawLine(spawnPoint.position, Mission.instance.PlayerFrame.transform.position, Color.blue);
                    Activated = true;
                }
            }
        }
        else
        {
            if(mySpawn.Count < MaxSpawn || CountSpawnTimeWhileMaxSpawns)
                timeSinceLastSpawn += Time.fixedDeltaTime;

            if (timeSinceLastSpawn >= SpawnTime && mySpawn.Count < MaxSpawn)
            {
                Spawn();
                timeSinceLastSpawn = 0.0f;
            }
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

}
