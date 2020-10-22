using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour {

    public string MobID;
    public GameObject DeathParticles;

    void OnDestroy()
    {
        
    }

    protected virtual void Die()
    {
        Mission.instance.BroadcastMessage("OnEnemyDestroyed", MobID);
        Object.Destroy(this.gameObject);
        GameObject.Instantiate(DeathParticles, transform.position, Quaternion.identity);
    }

    public virtual void ResetOrientation()
    {

    }
}
