using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentHealthTracker : MonoBehaviour {


    public FrameComponent TrackedComponent;
    public Image ComponentImage;
    public Color deadColor = Color.black;
    public Gradient DamageFade;

    private void FixedUpdate()
    {
        if (TrackedComponent != null)
            ComponentImage.color = TrackedComponent.IsDestroyed ? deadColor : DamageFade.Evaluate(1.0f - ((float)(TrackedComponent.ArmorPoints) / (float)(TrackedComponent.MaxArmor)));
    }
}
