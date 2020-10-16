using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionObjectiveDestroyEnemy : MissionObjective {

    public string EnemyID;
    public int TargetCount;
    int count;

    public override void OnMissionStart()
    {
        base.OnMissionStart();
        count = 0;
    }

    public void OnEnemyDestroyed(string enemyID)
    {
        if(isMissionActive && enemyID == EnemyID)
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
        string outString = string.Format(MissionTextFormat,EnemyID, count.ToString() + " / " + TargetCount.ToString());
        //outString.Replace("%1", EnemyID);
        //outString.Replace("%2", count.ToString() + " / " + TargetCount.ToString());
        return outString;
        //return EnemyID + " destroyed: " + count + " / " + TargetCount;
    }
}
