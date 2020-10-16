using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionObjectiveActivateTrigger : MissionObjective {

    public string TriggerID;
    public int ActivationsToCompelte;
    int ActivationCount;

    void ActivateTrigger(string triggerID)
    {
        if (isMissionActive && triggerID == TriggerID)
        {
            ActivationCount++;
            if (ActivationCount >= ActivationsToCompelte)
            {
                if (failOnComplete)
                    Mission.instance.ObjectiveFailed(this);
                else
                    Mission.instance.ObjectiveComplete(this);
            }
        }
    }

    public override void OnMissionStart()
    {
        base.OnMissionStart();
        ActivationCount = 0;
    }

    public override string GetMissionText()
    {
        return base.GetMissionText();
    }
}
