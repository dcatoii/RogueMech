using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActionShowPopup : TriggeredAction {

    public Popup ToShow;

    public override void Activate(Mob source)
    {
        ToShow.Show();
    }
}
