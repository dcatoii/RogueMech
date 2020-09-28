using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public GameObject NewGameButton;
    public GameObject ContinueButton;

    private void Start()
    {
        ContinueButton.SetActive(PlayerData.HasSavedData);
    }

    public void NewGame()
    {
        PlayerData.ClearPlayerData();
        
        //unlock the default parts
        PlayerData.UnlockPart(ApplicationContext.PlayerCustomizationData.DefaultLegs.gameObject.name);
        PlayerData.UnlockPart(ApplicationContext.PlayerCustomizationData.DefaultCore.gameObject.name);
        PlayerData.UnlockPart(ApplicationContext.PlayerCustomizationData.DefaultArms.gameObject.name);
        PlayerData.UnlockPart(ApplicationContext.PlayerCustomizationData.DefaultHead.gameObject.name);
        PlayerData.UnlockPart(ApplicationContext.PlayerCustomizationData.DefaultThruster.gameObject.name);
        PlayerData.UnlockPart(ApplicationContext.PlayerCustomizationData.DefaultRightWeapon.gameObject.name);

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
