using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MechEnergyHud : MonoBehaviour {

    public FrameCore TrackedCore;
    public Slider EnergySlider;

	// Update is called once per frame
	void FixedUpdate () {
        if (Mission.instance.PlayerFrame == null)
        {
            this.gameObject.SetActive(false);
            return;
        }

        EnergySlider.maxValue = TrackedCore.MaxEnergy;
        EnergySlider.value = TrackedCore.Energy;
	}
}
