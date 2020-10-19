using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSphereCannon : Mob {

    public ResetCannon Cannon;


    private void FixedUpdate()
    {
        if (Mission.instance.PlayerFrame == null || ApplicationContext.Game.IsPaused)
            return;

        Cannon.OnFireDown(Cannon.FirePoint.transform.position + transform.forward * 10.0f);
    }

}
