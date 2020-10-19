using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionTriggerAtInterval : MonoBehaviour {

    public string TriggerID;
    public float Interval;
    public bool IsLooping;
    public List<TriggeredAction> Actions;
    float age = 0;

    private void Start()
    {
        age = 0.0f;
    }

    public void Begin()
    {
        enabled = true;
        age = 0.0f;
    }

    private void FixedUpdate()
    {
        if (ApplicationContext.Game.IsPaused)
            return;

        age += Time.fixedDeltaTime;
        if(age >= Interval)
        {
            foreach (TriggeredAction action in Actions)
            {
                action.Activate(null);
            }

            if (TriggerID != "")
                Mission.instance.BroadcastMessage("IntervalPassed", TriggerID, SendMessageOptions.DontRequireReceiver);
            
            if (IsLooping)
            {
                age = 0.0f;
            }
            else
            {
                enabled = false;
            }
        }
    }

}
