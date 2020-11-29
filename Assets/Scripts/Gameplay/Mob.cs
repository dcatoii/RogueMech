using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour {

    public enum MobFaction
    {
        Ally,
        Enemy,
        Neutral,
    }

    public string MobID;
    public MobFaction Faction = MobFaction.Enemy;
    public GameObject DeathParticles;
    protected bool isDying = false;
    public virtual Transform targetPoint { get { return transform; } }

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
        ApplicationContext.AIManager.UnregisterMob(this);
    }

    public virtual void ResetOrientation()
    {

    }

    protected virtual void Start()
    {
        ApplicationContext.AIManager.RegisterMob(this);
    }


}
