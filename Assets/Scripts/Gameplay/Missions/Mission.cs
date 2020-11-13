using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour {

    public static Mission instance;
    public string MissionID;
    public List<MissionObjective> StartingGoals;
    List<MissionObjective> CompletedObjectives = new List<MissionObjective>();
    List<MissionObjective> CriticalActiveGoals = new List<MissionObjective>();
    public MissionHUD HUD;
    public MechFrame PlayerFrame;
    public int BaseAward = 25000;
    int BonusAmount = 0;
    public bool isTutorialMission = false;
    public float MissionTime = 0.0f;


	// Use this for initialization
	void Start ()
    {
        instance = this;
        //begin tracking the starting goals
        InitializeGoals(StartingGoals);
        //safety call to make sure no lingering pauses interfere
        ApplicationContext.Game.CurrentState = isTutorialMission ? GameContext.Gamestate.Tutorial : GameContext.Gamestate.Mission;
        ApplicationContext.Resume();

        //Load mission context information
        if(ApplicationContext.MissionData != null)
        {
            MissionID = ApplicationContext.MissionData.MissionID;
            BaseAward = ApplicationContext.MissionData.BaseReward;
            if (!ApplicationContext.MissionData.HasBeenCleared)
                BonusAmount += ApplicationContext.MissionData.FirstTimeBonus;
        }
    }

    private void InitializeGoals(List<MissionObjective> Goals)
    {
        if (Goals == null)
            return;

        foreach (MissionObjective objective in Goals)
        {
            objective.OnMissionStart();
            HUD.TrackMission(objective);
            //if this is a negative goal, assume compelte until it has failed
            if (objective.failOnComplete)
            {
                CompletedObjectives.Add(objective);
                BonusAmount += objective.ScoreValue;
            }
            else if (!objective.isBonusObjective) //if this is a positive goal, record that we are tracking it for mission completion
                CriticalActiveGoals.Add(objective);
        }
    }

    public void ObjectiveComplete(MissionObjective objective)
    {
        CompletedObjectives.Add(objective);
        objective.isMissionActive = false;
        BonusAmount += objective.ScoreValue;

        //initialize any subsequent objectives
        InitializeGoals(objective.SecondaryObjectives);
        //update the mission-critical objectives list if this is not a bonus objective
        if(!objective.isBonusObjective)
        {
            //remove from the list of active goals
            CriticalActiveGoals.Remove(objective);
            //determine if the mission is complete
            if (CriticalActiveGoals.Count == 0)
            {
                EndMission(true);
            }
        }

        HUD.UntrackMission(objective);

    }

    public void ObjectiveFailed(MissionObjective objective)
    {
        //if this is a negative goal, we assumed it succeeded and need remove it from the success list
        if (objective.failOnComplete)
        {
            CompletedObjectives.Remove(objective);
            BonusAmount -= objective.ScoreValue;
        }

        //if this is a mission-critical objective, failing it should fail the mission
        //Mission-Critical means that the objective is not a bonus objective and has no fallback when failed
        if (!objective.isBonusObjective && objective.SecondaryObjectives.Count == 0)
            EndMission(false);
        else //if this is not mission-critical, initialize any subsequent goals the objective may have
        {
            InitializeGoals(objective.SecondaryObjectives);
            objective.isMissionActive = false;
            HUD.UntrackMission(objective);
            if (objective.isBonusObjective)
                BadNotification("Bonus Objective Failed!");
            else
                BadNotification("Objective Failed!");
        }

    }

    public void EndMission (bool wasMissionSuccessful)
    {
        ApplicationContext.Game.IsPaused = true;
        ApplicationContext.UnlockCursor();

        if (wasMissionSuccessful)
        {
            //Mark Mission complete
            PlayerData.CompelteMission(MissionID);

            //add currency for mission, subtracting for damage taken
            int RepairCost = PlayerFrame.Core.MaxArmor - PlayerFrame.Core.ArmorPoints;
            RepairCost += PlayerFrame.Legs.MaxArmor - PlayerFrame.Legs.ArmorPoints;
            RepairCost += PlayerFrame.Head.MaxArmor - PlayerFrame.Head.ArmorPoints;
            RepairCost += PlayerFrame.Arms.MaxArmor - PlayerFrame.Arms.ArmorPoints;
            int FinalReward = BaseAward + BonusAmount - RepairCost;
            PlayerData.Currency += FinalReward;

            HUD.MissionSuccess(MissionTime, BaseAward, BonusAmount, RepairCost, FinalReward);
            PlayerFrame.GetComponent<PlayerInputManager>().enabled = false;

            //if this is the tutorial mission, mark the tutorial as cleared
            if (isTutorialMission)
                PlayerData.HasCompletedTutorial = true;
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
            ApplicationContext.OpenPauseMenu();
        }

        MissionTime += Time.fixedDeltaTime;
    }
}
