using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionSelectManager : MonoBehaviour {

    public MissionSelectMap Map;
    public MissionSelectCamera SelectCamera;
    MissionSelectCell highlightCell = null;
    MissionSelectCell selectedCell = null;

    // Use this for initialization
    void Start () {
        //Set up the game context for a menu state
        ApplicationContext.Game.CurrentState = GameContext.Gamestate.MissionSelect;
        ApplicationContext.Resume();
        ApplicationContext.UnlockCursor();	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (selectedCell != null) //missing info pane is up. 
            return;

        if(highlightCell != null)
            highlightCell.Highlight.SetActive(false);

        highlightCell = RaycastMap();

        if (highlightCell != null)
            highlightCell.Highlight.SetActive(true);

        if (highlightCell != null && Input.GetAxis("Submit") > 0)
        {
            SelectCell(highlightCell);
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
        selectedCell = selected;
        //For inital testing purposes. Change this to bring up mission info instead.
        StartMission();
    }

    void StartMission()
    {
        if (selectedCell == null)
            return;

        UnityEngine.SceneManagement.SceneManager.LoadScene(selectedCell.SceneName);
    }
}
