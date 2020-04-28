using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    public static int getRemainingEnemyCount()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

}
