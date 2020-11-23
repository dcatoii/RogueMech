using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour {

    public string MobID;
    public GameObject DeathParticles;
    protected bool isDying = false;

    void OnDestroy()
    {
        
    }

    protected virtual void Die()
    {
        //guard against weird issue where an enemy could die multiple times
        if (isDying)
            return;

        Mission.instance.BroadcastMessage("OnEnemyDestroyed", MobID, SendMessageOptions.DontRequireReceiver);
        Object.Destroy(this.gameObject);
        GameObject.Instantiate(DeathParticles, transform.position, Quaternion.identity);
        isDying = true;
    }

    public virtual void ResetOrientation()
    {

    }
}
