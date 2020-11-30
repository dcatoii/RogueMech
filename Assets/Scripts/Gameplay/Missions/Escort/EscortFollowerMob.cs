using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscortFollowerMob : EscortMob {

    public EscortMob Leader;

    protected override void FixedUpdate()
    {

        if(Leader == null || ApplicationContext.Game.IsPaused)
        {
            isMoving = false;
            return;
        }


        if(Leader.IsMoving)
        {
            MoveForward();
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }
}
