using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    public static float timescale = 1;

    public static int getRemainingEnemyCount()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

}
