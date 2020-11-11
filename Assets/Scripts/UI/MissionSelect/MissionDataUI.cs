using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionDataUI : MonoBehaviour {

	public TMPro.TMP_Text MissionName;
    public TMPro.TMP_Text MissionDetails;
    public Image MissionImage;
    public TMPro.TMP_Text Location;
    public TMPro.TMP_Text BaseReward;
    public TMPro.TMP_Text FirstTimeBonus;

    public void Show(MissionSelectCell selected)
    {
        MissionName.text = selected.MissionName;
        MissionDetails.text = selected.MissionDetails;
        //MissionImage;
        Location.text = "Location: " + selected.MissionLocation;
        BaseReward.text = "Reward: " + RogueMechUtils.intToCurrency(selected.BaseReward);
        FirstTimeBonus.text = "First-Time Bonus: " + (selected.HasBeenCleared ? "CLEARED" : RogueMechUtils.intToCurrency(selected.FirstTimeBonus));

        //TODO: Animate In
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        //TODO: Animate Out
        gameObject.SetActive(false);
    }
}
