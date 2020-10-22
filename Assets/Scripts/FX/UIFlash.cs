using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFlash : MonoBehaviour {

    public Image Target;
    public float duration = 0.25f;
    public Gradient ColorCurve;
    float age = 0f;

    private void Start()
    {
        ApplicationContext.Game.IsPaused = true;
    }

    // Update is called once per frame
    void Update () {
        age += Time.deltaTime;
        if (age < duration)
            Target.color = ColorCurve.Evaluate(age / duration);
        else
            GameObject.Destroy(this.gameObject);
	}

    private void OnDestroy()
    {
        ApplicationContext.Game.IsPaused = false;
    }
}
