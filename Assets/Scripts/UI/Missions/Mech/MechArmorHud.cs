using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MechArmorHud : MonoBehaviour {

    public FrameCore TrackedCore;
    public Slider ArmorSlider;

	// Update is called once per frame
	void FixedUpdate () {
        if (Mission.instance.PlayerFrame == null)
        {
            this.gameObject.SetActive(false);
            return;
        }

        ArmorSlider.maxValue = TrackedCore.MaxArmor;
        ArmorSlider.value = TrackedCore.ArmorPoints;
	}
}
