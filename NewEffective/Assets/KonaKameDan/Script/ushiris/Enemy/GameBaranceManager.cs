using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBaranceManager
{
    public static GameBaranceManager Instance { get; private set; } = new GameBaranceManager();

    public static float EnemyPopNear { get; private set; } = 25;
    public static float EnemyPopFar { get; private set; } = 45;
    public static float EnemyPopFast { get; private set; } = 5f;
    public static float EnemyPopLate { get; private set; } = 12f;

    public void SetEnemyPopNear(float value)
    {
        if (value <= EnemyPopFar) EnemyPopNear = value;
    }
    public void SetEnemyPopFar(float value)
    {
        if (value >= EnemyPopFar) EnemyPopFar = value;
    }
    public void SetEnemyPopFast(float value)
    {
        if (value <= EnemyPopLate) EnemyPopFast = value;
    }
    public void SetEnemyPopLate(float value)
    {
        if (value >= EnemyPopFast) EnemyPopLate = value;
    }
}
