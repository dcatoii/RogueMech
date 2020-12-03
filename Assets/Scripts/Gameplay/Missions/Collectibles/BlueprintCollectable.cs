using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintCollectable : Collectable {

    public FramePart UnlockablePrefab;
    public Collectable BackupifUnlocked;
    bool useBackup = false;
    private void Start()
    {
        useBackup = PlayerData.IsPartBlueprintUnlocked(UnlockablePrefab);
        gameObject.SetActive(!useBackup);
        
        if(BackupifUnlocked != null)
        {
            BackupifUnlocked.gameObject.SetActive(useBackup);
        }
    }

    protected override void Collect(Mob source)
    {
        if (useBackup)
        {
            BackupifUnlocked.Activate(source);
        }
        else
        {
            Mission.instance.BlueprintFound(UnlockablePrefab);
            Mission.instance.GoodNotification("Blueprint Found! Complete mission to unlock!");
        }
    }
}
