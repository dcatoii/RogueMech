using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour {

    public static Mission instance;
    public MissionObjective[] Missions;
    public MissionHUD HUD;
    public MechFrame PlayerFrame;
    public int BaseAward = 25000;

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
            //add currency for mission, subtracting for damage taken
            int RepairCost = PlayerFrame.Core.MaxArmor - PlayerFrame.Core.ArmorPoints;
            RepairCost += PlayerFrame.Legs.MaxArmor - PlayerFrame.Legs.ArmorPoints;
            RepairCost += PlayerFrame.Head.MaxArmor - PlayerFrame.Head.ArmorPoints;
            RepairCost += PlayerFrame.Arms.MaxArmor - PlayerFrame.Arms.ArmorPoints;
            int FinalReward = BaseAward - RepairCost;
            PlayerData.Currency += FinalReward;

            HUD.MissionSuccess();
            PlayerFrame.GetComponent<PlayerInputManager>().enabled = false;
        }
        else
            HUD.MissionFailed();


        PlayerFrame = null;
    }
}
