using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionObjectiveEscort : MissionObjective {

    public int TargetCount = 1;
    protected int count = 0;
    public string EscortID;

    public override void OnMissionStart()
    {
        base.OnMissionStart();
        count = 0;
    }

    public void OnEscortComplete(EscortMob eMob)
    {
        if (isMissionActive && eMob.MobID == EscortID)
        {
            count++;
            if (count >= TargetCount)
            {
                if (failOnComplete)
                    Mission.instance.ObjectiveFailed(this);
                else
                    Mission.instance.ObjectiveComplete(this);
            }
        }
    }

    public override string GetMissionText()
    {
        string outString = string.Format(MissionTextFormat, EscortID, count.ToString() + " / " + TargetCount.ToString());
        //outString.Replace("%1", EnemyID);
        //outString.Replace("%2", count.ToString() + " / " + TargetCount.ToString());
        return outString;
        //return EnemyID + " destroyed: " + count + " / " + TargetCount;
    }
}
