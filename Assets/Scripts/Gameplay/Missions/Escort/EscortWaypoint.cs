using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscortWaypoint : MonoBehaviour {

    public bool PlayerClose = false;
    public bool NoEnemies = false;
    public float Speed;

    public int FlagMask {  get { return EscortFlags.GetMask(NoEnemies, PlayerClose); } }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        EscortMob eMob = other.GetComponentInParent<EscortMob>();
        if (eMob != null)
            eMob.WaypointReached(this);
    }
}
