using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageSpinner : MonoBehaviour {

    public float rotateSpeed = 60.0f;

    private void FixedUpdate()
    {
        transform.Rotate(transform.up, rotateSpeed * Time.fixedDeltaTime);
    }

    public void Reset()
    {
        transform.rotation = Quaternion.identity;
    }
}
