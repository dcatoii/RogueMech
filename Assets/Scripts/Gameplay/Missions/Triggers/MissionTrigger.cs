using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionTrigger : MonoBehaviour {

    public string triggerID;


    public bool isActivatedByPlayer = true;
    public bool isActivatedByEnemy = false;
    public bool shouldActivate = true;
    public bool shouldDeactivate = false;

    public List<TriggeredAction> EnterActions;
    public List<TriggeredAction> ExitActions;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(shouldActivate)
            HandleCollision(other, "ActivateTrigger", EnterActions);
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (shouldDeactivate)
            HandleCollision(other, "DeactivateTrigger", ExitActions);
    }


    void HandleCollision(Collider other, string message, List<TriggeredAction> actions)
    {
        Mob unit = other.GetComponent<Mob>();
        if (unit == null)
            return;

        if (unit.tag == "Player")
        {
            if (!isActivatedByPlayer)
                return;
        }
        else if (!isActivatedByEnemy)
            return;
        

        if(message != "")
            Mission.instance.BroadcastMessage(message, triggerID, SendMessageOptions.DontRequireReceiver);

        foreach (TriggeredAction action in EnterActions)
        {
            action.Activate();
        }
    }
}
