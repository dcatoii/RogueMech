using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionSelectCell : MonoBehaviour {

    public GameObject HighlightEffect;
    public GameObject SelectEffect;
    public Transform CameraCenter;
    public Transform CameraFocus;

    public enum CellState
    {
        Hidden, // not visible
        Locked, // visible but not selectable
        Available, // visible, selectable
        Completed // visible,  and previously cleared
    };
    //this will be the correct way to do it
    //public MissionData MissionInfo;

    public MissionContext MissionInfo;

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
