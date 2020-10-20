using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour {

    public virtual void Close()
    {
        GameObject.Destroy(this.gameObject);
    }

    public virtual void Show()
    {
        GameObject.Instantiate(this, ApplicationContext.PopupRoot.transform);
    }
}
