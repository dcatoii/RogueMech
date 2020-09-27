using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData {

	public static int Currency {  get { return PlayerPrefs.GetInt("PlayerCurrency", 10000); }
        set { PlayerPrefs.SetInt("PlayerCurrency", value); }
    }

    public static bool IsPartUnlocked(string partID) { return PlayerPrefs.GetInt(partID + "_unlocked", 0) == 1; }
    public static void UnlockPart(string partID) { PlayerPrefs.SetInt(partID + "_unlocked", 1); }

    public static int GetPartUpgradesPurchased(string partID) { return PlayerPrefs.GetInt(partID + "_level", 0); }
    public static void PartUpgadePurchased(string partID) { PlayerPrefs.SetInt(partID + "_level", PlayerPrefs.GetInt(partID + "_level", 0)); }

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
