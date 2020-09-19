using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayMissionButton : MonoBehaviour {

	public void ReplayMission()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Mission_01");
    }
}
