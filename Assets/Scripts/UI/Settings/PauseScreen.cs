using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour {

    public GameObject RestartButton;
    public GameObject QuitMissionButton;

	// Use this for initialization
	void Start ()
    {
        RestartButton.SetActive(ApplicationContext.Game.CurrentState == GameContext.Gamestate.Mission);
        QuitMissionButton.SetActive(ApplicationContext.Game.CurrentState == GameContext.Gamestate.Mission);
    }

    public void Close()
    {
        GameObject.Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        ApplicationContext.Resume();
    }
}
