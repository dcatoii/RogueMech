﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscortMob : Mob {

    public EscortTriggerVolume EscortVolume;
    public EscortPath Path;
    int CurrentWaypointIndex = 0;
    public EscortWaypoint CurrentWaypoint { get { return Path.Waypoints[CurrentWaypointIndex]; } }
    bool hasReachedDestination = false;
    bool HasReachedDestination {  get { return hasReachedDestination; } }
    public float TurnSpeed = 0.1f;
    protected bool isMoving = false;
    public bool IsMoving {  get { return isMoving; } }

    protected override void Start()
    {
        hasReachedDestination = false;
        isMoving = false;
        CurrentWaypointIndex = 0;
        base.Start();
    }

    protected virtual void FixedUpdate()
    {
        if (ApplicationContext.Game.IsPaused || hasReachedDestination)
            return;

        if (CurrentWaypoint.FlagMask == EscortFlags.AutoScroll)
        {
            MoveForward(CurrentWaypoint.Speed);
        }
        else if ((CurrentWaypoint.FlagMask & EscortVolume.EscortFlag) == CurrentWaypoint.FlagMask)
        {
            MoveForward(CurrentWaypoint.Speed);
        }
        else
        {
            isMoving = false;
        }
    }

    protected void MoveForward(float speed)
    {
        TurnTowardsWaypoint();
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
        isMoving = true;
    }

    public void WaypointReached(EscortWaypoint waypoint)
    {
        if (HasReachedDestination || waypoint != CurrentWaypoint)
            return;

        CurrentWaypointIndex++;
        if (CurrentWaypointIndex >= Path.Waypoints.Count)
            OnDestinationReached();
    }

    protected void OnDestinationReached()
    {
        hasReachedDestination = true;
        isMoving = false;
        Mission.instance.BroadcastMessage("OnEscortComplete", this, SendMessageOptions.DontRequireReceiver);
    }

    protected void TurnTowardsWaypoint()
    {
        // Determine which direction to rotate towards
        Vector3 targetDirection = CurrentWaypoint.transform.position - transform.position;
        
        // The step size is equal to speed times frame time.
        float singleStep = TurnSpeed * Time.fixedDeltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        // Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}