using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandRotator : MonoBehaviour {

    public Vector3 MaxRotation = new Vector3(60f, 60f, 60f);
    public Vector3 MinRotation = new Vector3(-60f, -60f, -60f);

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 RotAmount = new Vector3(
            Random.Range(MaxRotation.x, MinRotation.x),
            Random.Range(MaxRotation.x, MinRotation.x),
            Random.Range(MaxRotation.x, MinRotation.x));

        gameObject.transform.Rotate(RotAmount * Time.deltaTime);
	}
}
