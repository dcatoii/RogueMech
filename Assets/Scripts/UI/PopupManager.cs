using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        ApplicationContext.PopupRoot = this;
    }
}
