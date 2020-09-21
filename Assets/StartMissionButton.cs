using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMissionButton : MonoBehaviour {

	public void StartMission()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Mission_01");
    }
}
