using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RogueMech/Game Context")]
public class GameContext : ScriptableObject {

    public enum Gamestate
    {
        MainMenu,
        Tutorial,
        Garage,
        Mission
    }

    public bool IsPaused;
    public Gamestate CurrentState;
}
