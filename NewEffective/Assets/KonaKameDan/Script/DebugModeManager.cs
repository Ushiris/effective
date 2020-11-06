﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

/// <summary>
/// デバッグ用
/// </summary>
public class DebugModeManager : MonoBehaviour
{
    [Header("エフェクト所持数を自由に変更可能")]
    [SerializeField] List<EffectObjectAcquisition.EffectObjectClass> effectCount = new List<EffectObjectAcquisition.EffectObjectClass>();
    [SerializeField] bool isEffectCount;
    [SerializeField] List<string> effectObjectName = new List<string>();

    [Header("クールタイムを変更する")]
    [SerializeField] bool isCoolTimeChange;
    [SerializeField] float coolTimeChange;

    [Header("敵のスポーンエリア")]
    [SerializeField] bool isEnemySpawnArea;
    [SerializeField] bool isEnemySpawnEnabled = true;

    // Start is called before the first frame update
    void Reset()
    {
        effectCount = new List<EffectObjectAcquisition.EffectObjectClass>();
        effectObjectName = new List<string>();
        var ed = EffectObjectID.effectDictionary;
        foreach (var key in ed.Keys)
        {
            effectCount.Add(new EffectObjectAcquisition.EffectObjectClass { name = ed[key], count = 1 });
            effectCount[effectCount.Count - 1].id = (int)key;
            effectObjectName.Add(ed[key]);
        }
    }

    // Update is called once per frame
    void OnValidate()
    {
        if (isEffectCount)
        {
            EffectObjectAcquisition.effectObjectAcquisition = new List<EffectObjectAcquisition.EffectObjectClass>(effectCount);
            EffectObjectAcquisition.effectObjectName = new List<string>(effectObjectName);
        }

        if (isCoolTimeChange)
        {
            PlayerArtsInstant.isDebugCoolTime = true;
            PlayerArtsInstant.debugCoolTime = coolTimeChange;
        }
        else PlayerArtsInstant.isDebugCoolTime = false;

        EnemySpawnPoint.isAreaEnabled = isEnemySpawnArea;
        EnemySpawnPoint.isSpawnEnabled = isEnemySpawnEnabled;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            EditorApplication.isPaused = true;
        }
    }
}
#endif