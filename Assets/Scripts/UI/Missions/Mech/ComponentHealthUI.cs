using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentHealthUI : MonoBehaviour {

    public MechFrame trackedFrame;
    public List<ComponentHealthTracker> CoreTrackers;
    public List<ComponentHealthTracker> HeadTrackers;
    public List<ComponentHealthTracker> LegTrackers;
    public List<ComponentHealthTracker> ArmTrackers;

    public void TrackMech(MechFrame tracked)
    {
        trackedFrame = tracked;

        foreach (ComponentHealthTracker tracker in CoreTrackers)
            tracker.TrackedComponent = trackedFrame.Core;

        foreach (ComponentHealthTracker tracker in HeadTrackers)
            tracker.TrackedComponent = trackedFrame.Head;

        foreach (ComponentHealthTracker tracker in LegTrackers)
            tracker.TrackedComponent = trackedFrame.Legs;

        foreach (ComponentHealthTracker tracker in ArmTrackers)
            tracker.TrackedComponent = trackedFrame.Arms;
    }
}
