using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscortFlags {
    public static int AutoScroll = 0;
    public static int NoEnemies = 1;
    public static int PlayerClose = 1 << 1;

    public static int GetMask(bool noEnemies = false, bool playerClose = false)
    {
        int mask = 0;

        if (noEnemies)
            mask += NoEnemies;
        if (playerClose)
            mask += PlayerClose;
        return mask;
    }
}
