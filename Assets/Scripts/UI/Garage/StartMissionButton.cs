using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMissionButton : MonoBehaviour {

    public WarningManager Warnings;

    public void StartMission()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/MissionSelect");
    }

    private void FixedUpdate()
    {
        GetComponent<Button>().interactable = (Warnings.WarningCount <= 0);
    }
}
