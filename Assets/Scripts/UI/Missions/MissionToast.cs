using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionToast : MonoBehaviour {

    public Image Background;
    public TMPro.TMP_Text Text;

    public float timeBeforeFade = 3.0f;
    public float fadeTime = 1.0f;
    float age = 0.0f;

	// Use this for initialization
	void Start () {
		
	}

    private void FixedUpdate()
    {
        age += Time.deltaTime;
        if(age > timeBeforeFade)
        {
            float fadeAge = age - timeBeforeFade;
            float alpha = Mathf.Lerp(0.0f, 1.0f, 1.0f - (fadeAge / fadeTime));

            //set BG alpha
            Color color = Background.color;
            color.a = alpha;
            Background.color = color;
            //set text alpha
            Text.alpha = alpha;

            if(fadeAge >= fadeTime)
            {
                GameObject.Destroy(this.gameObject);
            }
        }
    }
}
