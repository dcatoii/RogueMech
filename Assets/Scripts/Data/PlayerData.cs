using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData {

    public static bool HasSavedData
    {
        get { return PlayerPrefs.GetInt("ActiveSaveState", 0) == 1; }
        set { PlayerPrefs.SetInt("ActiveSaveState", value ? 1 : 0); }
    }

    public static bool HasCompletedTutorial
    {
        get { return PlayerPrefs.GetInt("HasCompletedTutorial", 0) == 1; }
        set { PlayerPrefs.SetInt("HasCompletedTutorial", value ? 1 : 0); }
    }

    public static int Currency {  get { return PlayerPrefs.GetInt("PlayerCurrency", 0); }
        set { PlayerPrefs.SetInt("PlayerCurrency", value); }
    }

    public static int DefaultViewMode
    {
        get { return PlayerPrefs.GetInt("DefaultViewMode", 0); }
        set { PlayerPrefs.SetInt("DefaultViewMode", value); }
    }

    public static float Sensitivity
    {
        get { return PlayerPrefs.GetFloat("Sensitivity", 6.0f); }
        set { PlayerPrefs.SetFloat("Sensitivity", value); }
    }

    public static bool IsPartUnlocked(string partID) { return PlayerPrefs.GetInt(partID + "_unlocked", 0) == 1; }
    public static void UnlockPart(string partID) { PlayerPrefs.SetInt(partID + "_unlocked", 1); }

    public static bool IsMissionComplete(string missionID) { return PlayerPrefs.GetInt(missionID + "_completed", 0) == 1; }
    public static void CompelteMission(string missionID) { PlayerPrefs.SetInt(missionID + "_completed", 1); }

    public static string GetPartUpgradeData(string partID)
    {
         return PlayerPrefs.GetString(partID + "_level", "");
    }
    public static void SetPartUpgradeData(string partID, string data)
    {
        PlayerPrefs.SetString(partID + "_level", data);
    }

    public static string CustomMechData
    {
        get { return PlayerPrefs.GetString("CustomMechData", ""); }
        set { PlayerPrefs.SetString("CustomMechData", value); }
    }

    public static void ClearPlayerData()
    {
        PlayerPrefs.DeleteAll();
    }

    /*
    Dictionary<string, bool> LevelCompleteFlags = new Dictionary<string, bool>();

    public bool isLevelComplete(string LevelName)
    {
        if(!LevelCompleteFlags.ContainsKey(LevelName))
        {
            LevelCompleteFlags.Add(LevelName, false);
        }
        return LevelCompleteFlags[LevelName];
    }

    public void SetLevelComplete(string LevelName)
    {
        LevelCompleteFlags[LevelName] = true;
    }
    */























}
