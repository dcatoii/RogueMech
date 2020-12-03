using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMob : Mob {

    public Mob Target;
    public bool Activated = false;
    public enum TargetModes
    {
        
        None, //Select no target
        Player, // Always choose the player as target
        Locked, // Stay on single target until it gets destroyed
        Random, // Choose a random target each cycle
        Nearest, // Choose nearest target each cycle
        Custom, //use child-specific implementation if one exists. Used as the default 
    }

    public TargetModes TargetMode =  TargetModes.Player;

    protected virtual void SelectCustomTarget()
    {
        //default choose nearest
        SelectNearestTarget();
    }

	protected void SelectTarget()
    {
        

        if (TargetMode == TargetModes.Locked)
        {
            //if our locked-on target is dead, we need to find a new one, 
            if (Target == null) 
                SelectCustomTarget();
            //otherwise do not change target
        }
        else if (TargetMode == TargetModes.None)
        {
            Target = null;
        }
        else if (TargetMode == TargetModes.Player)
        {
            Target = Mission.instance.PlayerFrame;
        }
        else if (TargetMode == TargetModes.Nearest)
        {
            SelectNearestTarget();
        }
        else if (TargetMode == TargetModes.Random)
        {
            SelectRandomTarget();
        }
        else if (TargetMode == TargetModes.Custom)
        {
            SelectCustomTarget();
        }
    }

    protected void SelectNearestTarget()
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

    protected void SelectPlayerAsTarget()
    {
        Target = Mission.instance.PlayerFrame;
    }

    protected void SelectRandomTarget()
    {
        List<Mob> TargetOptions = ApplicationContext.AIManager.GetValidTargets(this);
        if (TargetOptions.Count == 0)
            Target = null;

        int random = Random.Range(0, TargetOptions.Count);
        Target = TargetOptions[random];
    }

    public void ForceTarget(Mob target, bool locked = false)
    {
        Target = target;
        if (locked)
            TargetMode = TargetModes.Locked;
    }
}
