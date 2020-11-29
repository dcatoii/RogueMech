using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMob : Mob {
    public Weapon TankGun;
    public bool Activated = false;

   private void FixedUpdate()
    {
        if (Mission.instance.PlayerFrame == null || ApplicationContext.Game.IsPaused)
            return;

        if (!Activated)
        {
            Vector3 toPlayer = Mission.instance.PlayerFrame.gameObject.transform.position - transform.position;
            if (toPlayer.sqrMagnitude < (TankGun.FunctionalRange * TankGun.FunctionalRange))
            {
                //Line-of-Sight check
                if (!Physics.Linecast(TankGun.FirePoint.transform.position, Mission.instance.PlayerFrame.gameObject.transform.position, LayerMask.GetMask(new string[] { "Terrain" })))
                {
                    Activated = true;
                }
            }
        }

        else
        {
            TurnTowardsPlayer();
            TankGun.OnFireDown(Mission.instance.PlayerFrame.Core.gameObject.transform.position);
        }
    }

    void TurnTowardsPlayer()
    {
        Vector3 target = Mission.instance.PlayerFrame.gameObject.transform.position;
        target.y = transform.position.y;
        float strength = .5f;

        Quaternion targetRotation = Quaternion.LookRotation(target - transform.position);
        float str = Mathf.Min(strength * Time.deltaTime, 1);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
    }

}
