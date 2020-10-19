using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PusherPiston : MonoBehaviour {

    float age = 0;
    public Vector3 MinScale = Vector3.zero;
    public Vector3 MaxScale = Vector3.zero;

    public float UpTime;
    public float PauseUpTime;
    public float DownTime;
    public float PauseDownTime;

    protected void Update()
    {
        if (ApplicationContext.Game.IsPaused)
            return;

        age += Time.deltaTime;
        float cycleTime = UpTime + PauseUpTime + DownTime + PauseDownTime;
        while(age > cycleTime)
        {
            age -= cycleTime;
        }

        if (age < UpTime)
        {
            gameObject.transform.localScale = Vector3.Lerp(MinScale, MaxScale, Mathf.Clamp(age / UpTime, 0.0f, 1.0f));
        }
        else if (age < (UpTime + PauseUpTime))
        {
            gameObject.transform.localScale = MaxScale;
        }
        else if (age < (UpTime + PauseUpTime + DownTime))
        {
            float t = age - (UpTime + PauseUpTime);
            gameObject.transform.localScale = Vector3.Lerp(MaxScale, MinScale, Mathf.Clamp(t / DownTime, 0.0f, 1.0f));
        }
        else 
        {
            gameObject.transform.localScale = MinScale;
        }
        /* no auto-destroy (yet)
        if (age > lifeTime)
        {
            Object.Destroy(this.gameObject);
            GameObject.Instantiate(deathEffect, transform.position, Quaternion.identity);
        }*/
    }
}
