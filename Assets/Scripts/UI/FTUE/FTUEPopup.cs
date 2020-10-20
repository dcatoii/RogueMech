using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUEPopup : Popup {
    
    // Use this for initialization
    void Start()
    {
        ApplicationContext.Pause();
    }

    private void OnDestroy()
    {
        ApplicationContext.Resume();
    }
}
