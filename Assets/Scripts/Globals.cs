using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    static float score;
    public static int getRemainingEnemyCount()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    public static void resetScore()
    {
        Time.timeScale = 1;
        score = 0;
    }

    public static void addScore(float inScore)
    {
        score += inScore;
        Debug.Log("SCORE: " + score);
    }

    public static float getScore()
    {
        return score;
    }

}
