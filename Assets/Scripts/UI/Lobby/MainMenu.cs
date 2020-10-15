using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public GameObject NewGameButton;
    public GameObject ContinueButton;

    private void Start()
    {
        ContinueButton.SetActive(PlayerData.HasSavedData);
        ApplicationContext.Game.CurrentState = GameContext.Gamestate.MainMenu;
    }

    public void NewGame()
    {
        PlayerData.ClearPlayerData();
        
        //unlock the default parts
        PlayerData.UnlockPart(ApplicationContext.PlayerCustomizationData.DefaultLegs.PartID);
        PlayerData.UnlockPart(ApplicationContext.PlayerCustomizationData.DefaultCore.PartID);
        PlayerData.UnlockPart(ApplicationContext.PlayerCustomizationData.DefaultArms.PartID);
        PlayerData.UnlockPart(ApplicationContext.PlayerCustomizationData.DefaultHead.PartID);
        PlayerData.UnlockPart(ApplicationContext.PlayerCustomizationData.DefaultThruster.PartID);
        PlayerData.UnlockPart(ApplicationContext.PlayerCustomizationData.DefaultRightWeapon.PartID);

        //create save data
        PlayerData.HasSavedData = true;

        //start the first mission
        UnityEngine.SceneManagement.SceneManager.LoadScene("Mission_01");

    }

    public void Continue()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Garage");
    }

    
}
