using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultViewController : MonoBehaviour {

    public TMPro.TMP_Text Text;

	public void OnCycleNext()
    {
        if(PlayerData.DefaultViewMode == 0)
        {
            PlayerData.DefaultViewMode = 1;
            Text.text = "First";
        }
        else
        {
            PlayerData.DefaultViewMode = 0;
            Text.text = "Third";
        }
    }

    public void OnCyclePrev()
    {
        if (PlayerData.DefaultViewMode == 0)
        {
            PlayerData.DefaultViewMode = 1;
            Text.text = "First";
        }
        else
        {
            PlayerData.DefaultViewMode = 0;
            Text.text = "Third";
        }
    }
}
