﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionHUD : MonoBehaviour {

    public MissionText textPrefab;
    public Transform textListRoot;

    public GameObject MissionFailurePopup;
    public MissionCompletePopup CompletePopup;

    Dictionary<MissionObjective, MissionText> trackedMissions = new Dictionary<MissionObjective, MissionText>();

    public void TrackMission(MissionObjective objective)
    {
        if (trackedMissions.ContainsKey(objective))
            return;
        else
        {
            GameObject newTextObj = GameObject.Instantiate(textPrefab.gameObject, textListRoot);
            MissionText newText = newTextObj.GetComponent<MissionText>();
            newText.objective = objective;
            trackedMissions.Add(objective, newText);
        }
    }
    public void UntrackMission(MissionObjective objective)
    {
        if (!trackedMissions.ContainsKey(objective))
            return;

        GameObject.Destroy(trackedMissions[objective].gameObject);
        trackedMissions.Remove(objective);
    }

    public void MissionFailed()
    {
        GameObject.Instantiate(MissionFailurePopup, transform);
    }

    public void MissionSuccess(int reward, int bonus, int repair, int total)
    {

        MissionCompletePopup popup = GameObject.Instantiate(CompletePopup.gameObject, transform).GetComponent<MissionCompletePopup>();
        popup.RewardValue = reward;
        popup.BonusValue = bonus;
        popup.RepairValue = repair;
        popup.TotalValue = total;
    }
}