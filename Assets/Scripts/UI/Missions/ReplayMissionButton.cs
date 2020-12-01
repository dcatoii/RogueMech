using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayMissionButton : MonoBehaviour {

	public void ReplayMission()
    {
        //reload current mission scene
        ApplicationContext.AIManager.Reset(); //clear the AI manager
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
