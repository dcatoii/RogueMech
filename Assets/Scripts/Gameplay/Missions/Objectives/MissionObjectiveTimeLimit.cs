﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionObjectiveTimeLimit : MissionObjective {

    public float TimeLimit;
    float TimeElapsed;

    public override void OnMissionStart()
    {
        base.OnMissionStart();
        TimeElapsed = 0.0f;
    }

    private void FixedUpdate()
    {
        if (!isMissionActive || ApplicationContext.Game.IsPaused)
            return;

        TimeElapsed += Time.fixedDeltaTime;
        if (TimeElapsed >= TimeLimit)
        {
            if (failOnComplete)
                Mission.instance.ObjectiveFailed(this);
            else
                Mission.instance.ObjectiveComplete(this);
        }
    }

    public override string GetMissionText()
    {
        //string outString = MissionTextFormat;

        int c = Mathf.CeilToInt(Mathf.Clamp(TimeLimit - TimeElapsed, 0.0f, TimeLimit));
        int sec = c % 60;
        int min = c / 60;
        int hr = c / 3600;
        string timeString = (hr > 0) ? hr.ToString() + " : " : "";
        timeString += ((min > 9) ? "" : "0") + min.ToString() + " : ";
        timeString += ((sec > 9) ? "" : "0") + sec.ToString();


        //outString.Replace("%1", timeString);
        string outString = string.Format(MissionTextFormat, timeString);
        return outString;
        //return "Time Remaining:" + timeString;
    }
}