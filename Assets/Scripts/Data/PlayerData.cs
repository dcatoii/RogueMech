using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData {

	public static int Currency {  get { return PlayerPrefs.GetInt("PlayerCurrency", 10000); }
        set { PlayerPrefs.SetInt("PlayerCurrency", value); }
    }

    public static bool IsPartUnlocked(string partID) { return false; }

    public static string CustomMechData
    {
        get { return PlayerPrefs.GetString("CustomMechData", ""); }
        set { PlayerPrefs.SetString("CustomMechData", value); }
    }

    public static void ClearPlayerData()
    {
        PlayerPrefs.DeleteAll();
    }

}
