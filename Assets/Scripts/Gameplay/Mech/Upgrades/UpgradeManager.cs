using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager {

	public static void ApplyUpgrades(FramePart Part)
    {

        UpgradeData data = LoadUpgradeData(Part);

        //unapply current upgrades
        foreach (PartUpgrade upgrade in Part.AppliedUpgrades)
            upgrade.Remove(Part);

        Part.AppliedUpgrades.Clear();

        //for each upgrade level we have unlocked
        for(int levelIter = 0; levelIter < data.UpgradeLevel; levelIter++)
        {
            //if this level is higher than we have upgrade levels, there is a problem
            if (levelIter >= Part.UpgradeLevels.Count)
            {
                Debug.LogError("Level " + levelIter.ToString() + " does not exists for this part");
                return;
            }
            //if the upgrade option is a valid value
            if(data.UpgradeOptions[levelIter] != -1 && data.UpgradeOptions[levelIter] < Part.UpgradeLevels[levelIter].Options.Length)
            {
                //apply the upgrade
                Part.UpgradeLevels[levelIter].Options[data.UpgradeOptions[levelIter]].Apply(Part);
                Part.AppliedUpgrades.Add(Part.UpgradeLevels[levelIter].Options[data.UpgradeOptions[levelIter]]);
            }
        }
    }

    public static void UnlockUpgradeLevel(FramePart Part)
    {
        //load part's current upgrade info
        UpgradeData data = LoadUpgradeData(Part);

        //do nothing if this upgrade is already max level
        if (data.UpgradeLevel >= Part.UpgradeLevels.Count)
            return;
        
        //increment the upgrade level
        data.UpgradeLevel++;
        
        //save the upgrade
        SaveUpgradeData(Part, data);
    }

    public static void SwapUpgrade(FramePart Part, int level, int option)
    {
        //load part's current upgrade info
        UpgradeData data = LoadUpgradeData(Part);

        //do nothing if this upgrade is already applied
        if (data.UpgradeOptions[level] == option)
            return;

        //remove old upgrade
        //if (data.UpgradeOptions[level] != -1)
        //    Part.UpgradeLevels[level].Options[data.UpgradeOptions[level]].Remove(Part);

        //update upgrade info
        data.UpgradeOptions[level] = option;

        //apply new upgrade
        //if (data.UpgradeOptions[level] != -1)
        //    Part.UpgradeLevels[level].Options[data.UpgradeOptions[level]].Apply(Part);

        //Save the update
        SaveUpgradeData(Part, data);
    }

    public static UpgradeData LoadUpgradeData(FramePart part)
    {
        UpgradeData data = JsonConvert.DeserializeObject<UpgradeData>(PlayerData.GetPartUpgradeData(part.PartID));
        if (data == null)
            data = new UpgradeData(part.UpgradeLevels.Count);

        return data;
    }

    static void SaveUpgradeData(FramePart part, UpgradeData data)
    {
        string newData = JsonConvert.SerializeObject(data);
        PlayerData.SetPartUpgradeData(part.PartID, newData);
    }
}
