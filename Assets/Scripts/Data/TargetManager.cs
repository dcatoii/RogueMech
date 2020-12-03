using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager {

    private Dictionary<Mob.MobFaction, List<Mob>> ValidTargets = new Dictionary<Mob.MobFaction, List<Mob>>();
    
    public void RegisterMob(Mob mob)
    {
        if (ValidTargets[mob.Faction].Contains(mob))
            return;
        else
            ValidTargets[mob.Faction].Add(mob);

    }

    public void UnregisterMob(Mob mob)
    {
        while(ValidTargets[mob.Faction].Contains(mob))
            ValidTargets[mob.Faction].Remove(mob);
    }

    public List<Mob> GetValidTargets(Mob mob)
    {
        List<Mob> returnList = new List<Mob>();

        foreach(Mob.MobFaction key in ValidTargets.Keys)
        {
            if (key != mob.Faction)
                returnList.AddRange(ValidTargets[key]);
        }

        return returnList;
    }
	
    void Init()
    {
        ValidTargets.Add(Mob.MobFaction.Ally, new List<Mob>());
        ValidTargets.Add(Mob.MobFaction.Enemy, new List<Mob>());
        ValidTargets.Add(Mob.MobFaction.Neutral, new List<Mob>());
    }

    public void Reset()
    {
        ValidTargets.Clear();
        Init();
    }
}
