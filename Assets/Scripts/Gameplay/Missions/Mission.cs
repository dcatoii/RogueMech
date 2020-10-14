using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour {

    public static Mission instance;
    public MissionObjective[] Missions;
    public MissionHUD HUD;
    public MechFrame PlayerFrame;
    public int BaseAward = 25000;
    int BonusAmount = 0;

	// Use this for initialization
	void Start () {
        instance = this;
        foreach(MissionObjective objective in Missions)
        {
            objective.OnMissionStart();
            HUD.TrackMission(objective);
        }
        //safety call to make sure no lingering pauses interfere
        ApplicationContext.Game.CurrentState = GameContext.Gamestate.Mission;
        ApplicationContext.Resume();

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
        ApplicationContext.Game.IsPaused = true;
        Cursor.visible = true;

        if (wasMissionSuccessful)
        {
            //add currency for mission, subtracting for damage taken
            int RepairCost = PlayerFrame.Core.MaxArmor - PlayerFrame.Core.ArmorPoints;
            RepairCost += PlayerFrame.Legs.MaxArmor - PlayerFrame.Legs.ArmorPoints;
            RepairCost += PlayerFrame.Head.MaxArmor - PlayerFrame.Head.ArmorPoints;
            RepairCost += PlayerFrame.Arms.MaxArmor - PlayerFrame.Arms.ArmorPoints;
            int FinalReward = BaseAward + BonusAmount - RepairCost;
            PlayerData.Currency += FinalReward;

            HUD.MissionSuccess(BaseAward, BonusAmount, RepairCost, FinalReward);
            PlayerFrame.GetComponent<PlayerInputManager>().enabled = false;
        }
        else
            HUD.MissionFailed();


        PlayerFrame = null;
    }

    public void BadNotification(string Text)
    {
        HUD.Toasts.ShowBadToast(Text);
    }
    public void GoodNotification(string Text)
    {
        HUD.Toasts.ShowGoodToast(Text);
    }
    public void NeutralNotification(string Text)
    {
        HUD.Toasts.ShowNeutralToast(Text);
    }

    private void FixedUpdate()
    {
        if(ApplicationContext.Game.IsPaused)
            return;

       if (Input.GetAxis("Cancel") > 0)
        {
            ApplicationContext.Pause();
        }
    }
}
