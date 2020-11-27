using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscortTriggerVolume : MonoBehaviour {

    List<Mob> enemiesInVolume = new List<Mob>();
    List<Mob> alliesInVolume = new List<Mob>();

    public List<Mob> EnemiesInVolume { get { return enemiesInVolume; } }
    public List<Mob> AlliesInVolume {  get { return alliesInVolume; } }
    public bool isAllyPresent {  get { return alliesInVolume.Count > 0; } }
    public bool isEnemyPresent { get { return enemiesInVolume.Count > 0; } }
    public int EscortFlag { get { return EscortFlags.GetMask(!isEnemyPresent, isAllyPresent); } }


    protected void OnTriggerEnter(Collider other)
    {
        Mob mob = other.GetComponentInParent<Mob>();

        if (mob == null)
            return;

        if (mob.tag == "Player")
            AlliesInVolume.Add(mob);
        else
            enemiesInVolume.Add(mob);
    }

    protected void OnTriggerExit(Collider other)
    {
        Mob mob = other.GetComponentInParent<Mob>();

        if (mob == null)
            return;

        if (mob.tag == "Player")
            AlliesInVolume.Remove(mob);
        else
            enemiesInVolume.Remove(mob);
    }

    protected void FixedUpdate()
    {
        //clear out units that have died
        while (enemiesInVolume.Contains(null))
            enemiesInVolume.Remove(null);

        while (alliesInVolume.Contains(null))
            alliesInVolume.Remove(null);
    }
}
