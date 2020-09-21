using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHardAttach : MonoBehaviour {

    public Transform target { get { return transform.parent; } set { transform.parent = value; transform.localPosition = Vector3.zero; transform.localRotation = Quaternion.identity; transform.localScale = Vector3.one; } }
	
	// Update is called once per frame
	void FixedUpdate () {
        //if (target != null)
        //    transform.position = target.position;
	}
}
