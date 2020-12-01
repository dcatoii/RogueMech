using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMob : AIMob {
    public Weapon TankGun;

   private void FixedUpdate()
    {
        if (Mission.instance.PlayerFrame == null || ApplicationContext.Game.IsPaused)
            return;

        if (!Activated)
        {
            SelectTarget();
            if (Target == null)
                return;
            Vector3 toTarget = Target.transform.position - transform.position;
            if (toTarget.sqrMagnitude < (TankGun.FunctionalRange * TankGun.FunctionalRange))
            {
                //Line-of-Sight check
                if (!Physics.Linecast(TankGun.FirePoint.transform.position, Mission.instance.PlayerFrame.gameObject.transform.position, LayerMask.GetMask(new string[] { "Terrain" })))
                {
                    Activated = true;
                }
            }
        }

        else if (Target != null)
        {
            if (Target == null)
            {
                SelectTarget();
                return;
            }

            TurnTowardsTarget();
            if (TankGun.TimeSinceLastFire > TankGun.RefireTime)
            {
                TankGun.OnFireDown(Target.transform.position);
                SelectTarget();
            }
        }
        else
        {
            SelectTarget();
        }
    }

    void TurnTowardsTarget()
    {
        Vector3 target = Target.transform.position;
        target.y = transform.position.y;
        float strength = .5f;

        Quaternion targetRotation = Quaternion.LookRotation(target - transform.position);
        float str = Mathf.Min(strength * Time.deltaTime, 1);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
    }

    protected override void CoreDamaged(int amount)
    {
        Activated = true;
        base.CoreDamaged(amount);
    }

}
