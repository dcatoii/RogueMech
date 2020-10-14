using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RogueMech/Application Context")]
public class ApplicationContext : ScriptableObject
{
    private static ApplicationContext instance;

    public MechPartLibrary Lib;
    public static MechPartLibrary PartLibrary { get { return instance.Lib; } }

    public MechCustomizationData playerCustomizationData;
    public static MechCustomizationData PlayerCustomizationData { get { return instance.playerCustomizationData; } }

    public GameContext gameContext;
    public static GameContext Game { get { return instance.gameContext; } }

    public static PopupManager popupRoot;
    public static PopupManager PopupRoot { get { return popupRoot; } set { popupRoot = value; } }

    [SerializeField]
    PauseScreen PausePrefab;

    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        instance = Resources.LoadAll<ApplicationContext>("")[0];
    }

    public static void Pause()
    {
        if (Game.IsPaused)
            return;

        Game.IsPaused = true;
        GameObject.Instantiate(instance.PausePrefab, PopupRoot.transform);
        Time.timeScale = 0f;
    }

    public static void Resume()
    {
        Time.timeScale = 1f;
        Game.IsPaused = false;
    }

}
