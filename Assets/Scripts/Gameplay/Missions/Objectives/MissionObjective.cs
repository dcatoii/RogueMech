using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionObjective : MonoBehaviour {

    public int ScoreValue = 0;
    public bool isBonusObjective = false;
    public bool isMissionActive = false;
    public bool failOnComplete = false;
    public string MissionTextFormat;
    public List<MissionObjective> SecondaryObjectives;
    public List<TriggeredAction> ObjectiveStartTriggers;


    public virtual void OnMissionStart()
    {
        isMissionActive = true;
        foreach (TriggeredAction action in ObjectiveStartTriggers)
            action.Activate(null);
    }

    public virtual string GetMissionText()
    {
        return MissionTextFormat;
    }
}
