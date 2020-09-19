using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionText : MonoBehaviour {

    public MissionObjective objective;
    public TextMeshProUGUI Text;

    private void FixedUpdate()
    {
        Text.text = objective.GetMissionText();
    }
}
