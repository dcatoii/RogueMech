using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionSelectManager : MonoBehaviour {

    public MissionSelectMap Map;
    public MissionSelectCamera SelectCamera;
    public MissionDataUI MissionData;
    LTDescr ltAnimation = null;
    MissionSelectCell highlightCell = null;
    MissionSelectCell selectedCell = null;

    // Use this for initialization
    void Start () {
        //Set up the game context for a menu state
        ApplicationContext.Game.CurrentState = GameContext.Gamestate.MissionSelect;
        ApplicationContext.Resume();
        ApplicationContext.UnlockCursor();
        Map.LoadMap();
        SelectCamera.transform.position = Map.StartingCell.CameraCenter.position;
        MissionData.gameObject.SetActive(false);
        SelectCell(Map.StartingCell);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        //if (selectedCell != null) //missing info pane is up. 
        //  return;

        if (ApplicationContext.Game.IsPaused)
            return;

        if (highlightCell != null)
            highlightCell.HighlightEffect.SetActive(false);

        highlightCell = RaycastMap();

        if (highlightCell != null && highlightCell != selectedCell)
            highlightCell.HighlightEffect.SetActive(true);

        if (highlightCell != null && Input.GetAxis("MapClick") > 0)
        {
            SelectCell(highlightCell);
        }

        if (Input.GetAxis("Cancel") > 0)
        {
            ApplicationContext.OpenPauseMenu();
        }
    }

    MissionSelectCell RaycastMap()
    {
        MissionSelectCell hitCell = null;
        RaycastHit CameraRayInfo;

        
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(mouseRay, out CameraRayInfo, 1000.0f))
        {
            hitCell = CameraRayInfo.collider.GetComponent<MissionSelectCell>();
        }        
        return hitCell;
    }

    void SelectCell (MissionSelectCell selected)
    {
        //only 1 selected cell at a time
        if (ltAnimation != null)
        {
            ltAnimation.pause();
            ltAnimation.callOnCompletes();
        }

        //turn off the old selection, if any
        UnselectCell();

        selectedCell = selected;
        selectedCell.SelectEffect.SetActive(true);
        ltAnimation = LeanTween.move(SelectCamera.gameObject, selectedCell.CameraFocus.position, 0.5f);
        ltAnimation.setOnComplete(ShowMissionData);
    }

    void ShowMissionData()
    {
        MissionData.Show(selectedCell);
        AnimationComplete();
    }

    public void StartMission()
    {
        if (selectedCell == null)
            return;

        ApplicationContext.MissionData = selectedCell.MissionInfo;
        ApplicationContext.AIManager.Reset(); // clear the AI manager for the new level
        UnityEngine.SceneManagement.SceneManager.LoadScene(selectedCell.MissionInfo.SceneName);
    }

    public void CloseMissionInfo()
    {
        MissionData.Hide();
        if (ltAnimation != null)
        {
            ltAnimation.pause();
            ltAnimation.callOnCompletes();
        }

        ltAnimation = LeanTween.move(SelectCamera.gameObject, selectedCell.CameraCenter.position, 1.0f);
        UnselectCell();
        ltAnimation.setOnComplete(AnimationComplete);


    }

    private void UnselectCell()
    {
        if (selectedCell != null)
            selectedCell.SelectEffect.SetActive(false);

        selectedCell = null;
    }

    private void AnimationComplete()
    {
        ltAnimation = null;
    }
}
