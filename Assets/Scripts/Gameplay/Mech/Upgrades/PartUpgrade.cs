using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartUpgrade : MonoBehaviour {

    public Sprite StoreImage;
    public string StoreDescription;

    public virtual void Apply(FramePart Part)
    {
        
    }

    public virtual void Remove(FramePart Part)
    {

    }
}

public class UpgradeData
{
    public int UpgradeLevel;
    public int[] UpgradeOptions;

    public UpgradeData(int levels)
    {
        UpgradeLevel = 0;
        UpgradeOptions = new int[levels];
        for (int ii = 0; ii < levels; ii++)
            UpgradeOptions[ii] = -1;
    }
}
