﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 
/// 世界のレベル 
/// </summary> 
public class WorldLevel : MonoBehaviour
{
    [SerializeField] float levelUpTimeScale = 60f;

    static float tmpLevelUpTimeScale;

    /// <summary> 
    /// 世界のレベルを返す 
    /// </summary> 
    public static int GetWorldLevel { get; private set; } = 1;

    // Start is called before the first frame update 
    void Start()
    {
        if (GetWorldLevel == 0 || MainGameManager.GetArtsReset)
        {
            WorldLevelReset();
            tmpLevelUpTimeScale = levelUpTimeScale;
        }
    }

    // Update is called once per frame 
    void Update()
    {
        //レベルアップ 
        if (tmpLevelUpTimeScale < WorldTime.GetWorldTime)
        {
            GetWorldLevel++;
            tmpLevelUpTimeScale += LevelUpTimeScaleConversion();
        }
    }

    //次レベルアップに必要な時間を足す 
    float LevelUpTimeScaleConversion()
    {
        return levelUpTimeScale;
    }

    /// <summary> 
    /// レベルのリセット 
    /// </summary> 
    public static void WorldLevelReset()
    {
        GetWorldLevel = 1;
    }

    /// <summary> 
    /// レベルの強制変更 
    /// </summary> 
    /// <param name="num"></param> 
    public static void WorldLevelModification(int num)
    {
        GetWorldLevel = num;
    }
}