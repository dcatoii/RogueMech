using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionSelectCell : MonoBehaviour {

    public GameObject Highlight;

    public struct MissionData
    {
        public string MissionName;
        public string SceneName;
        public string MissionDetails;
        public int BaseReward;
        public int FirstTimeBonus;
        public bool HasBeenCleared;
    }

    public enum CellState
    {
        Hidden, // not visible
        Locked, // visible but not selectable
        Available, // visible, selectable
        Completed // visible,  and previously cleared
    };
    //this will be the correct way to do it
    //public MissionData MissionInfo;

    //Temp for variable testing
    public string MissionName = "Weaken Their Defenses";
    public string SceneName = "Mission_01";
    public string MissionDetails = "";
    public int BaseReward = 15000;
    public int FirstTimeBonus = 10000;
    public bool HasBeenCleared = false;

    public List<MissionSelectCell> Connections;
    public CellState CurrentState = CellState.Hidden;
    public

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
