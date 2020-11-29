using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMob : Mob {

    public Mob Target;
    public bool Activated = false;

	protected virtual void SelectTarget()
    {
        SelectRandomTarget();
    }

    public void SelectNearestTarget()
    {
        List<Mob> TargetOptions = ApplicationContext.AIManager.GetValidTargets(this);
        Target = null;
        float shortestDist = 0f;
        foreach(Mob option in TargetOptions)
        {
            
            if(Target == null)
            {
                shortestDist = (option.targetPoint.position - transform.position).sqrMagnitude;
                Target = option;
            }
            else
            {
                float distToTarget = (option.targetPoint.position - transform.position).sqrMagnitude;
                if (distToTarget < shortestDist)
                {
                    shortestDist = distToTarget;
                    Target = option;
                }
            }
        }
    }

    public void SelectPlayerAsTarget()
    {
        Target = Mission.instance.PlayerFrame;
    }

    public void SelectRandomTarget()
    {
        List<Mob> TargetOptions = ApplicationContext.AIManager.GetValidTargets(this);
        if (TargetOptions.Count == 0)
            Target = null;

        int random = Random.Range(0, TargetOptions.Count);
        Target = TargetOptions[random];
    }

    public void ForceTarget(Mob target)
    {
        Target = target;
    }
}
