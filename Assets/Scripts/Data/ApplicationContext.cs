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

    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        instance = Resources.LoadAll<ApplicationContext>("")[0];
    }

}
