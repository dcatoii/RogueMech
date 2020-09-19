using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour {

    public static Mission instance;
    public MissionObjective[] Missions;
    public MissionHUD HUD;
    public MechFrame PlayerFrame;


	// Use this for initialization
	void Start () {
        instance = this;
        foreach(MissionObjective objective in Missions)
        {
            objective.OnMissionStart();
            HUD.TrackMission(objective);
        }
	}

    public void ObjectiveComplete(MissionObjective objective)
    {
        EndMission(true);

    }

    public void ObjectiveFailed(MissionObjective objective)
    {
        EndMission(false);
    }

    public void EndMission (bool wasMissionSuccessful)
    {

        Cursor.visible = true;

        if (wasMissionSuccessful)
        {
            HUD.MissionSuccess();
            PlayerFrame.GetComponent<PlayerInputManager>().enabled = false;
        }
        else
            HUD.MissionFailed();


        PlayerFrame = null;
    }
}
