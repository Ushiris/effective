using System.Collections;
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

    [Header("ダメージを有効にする")]
    [SerializeField] bool isAllDamage = true;
    [SerializeField] bool isAllDamageZone = true;

    [Header("Mapの操作")]
    [SerializeField] GameObject stageData;
    [SerializeField] bool isDebugMap = false;
    [SerializeField] NewMap.MapType startMapType = NewMap.MapType.Grassland;

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
            EffectObjectAcquisition.isDefaultStatusReset = false;
            EffectObjectAcquisition.effectObjectAcquisition = new List<EffectObjectAcquisition.EffectObjectClass>(effectCount);
            EffectObjectAcquisition.effectObjectName = new List<string>(effectObjectName);

            //バッグの方の設定
            for (int i = 0; i < effectCount.Count; i++)
            {
                var name = (NameDefinition.EffectName)effectCount[i].id;
                var count = effectCount[i].count;

                var effectBag = EffectObjectAcquisition.GetEffectBag;
                if (effectBag.ContainsKey(name))
                {
                    effectBag[name] = count;
                }
            }

        }
        else
        {
            EffectObjectAcquisition.isDefaultStatusReset = true;
        }

        if (isCoolTimeChange)
        {
            PlayerArtsInstant.isDebugCoolTime = true;
            PlayerArtsInstant.debugCoolTime = coolTimeChange;
        }
        else PlayerArtsInstant.isDebugCoolTime = false;

        EnemySpawnPoint.isAreaEnabled = isEnemySpawnArea;
        EnemySpawnPoint.isSpawnEnabled = isEnemySpawnEnabled;

        ParticleHit.isAllDamageEnable = isAllDamage;
        ParticleHitZoneDamage.isAllDamageEnable = isAllDamageZone;

        if (isDebugMap)
        {
            stageData.SetActive(true);
            MenuStage.isDebug = true;
            NewMap.SetSelectMapType = NewMap.MapType.DebugMap;
        }
        else
        {
            stageData.SetActive(false);
            MenuStage.isDebug = false;
            //NewMap.SetSelectMapType = startMapType;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            EditorApplication.isPaused = true;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            DebugLogger.Log("称号: " + ResultScore.GetRankName());

            var id= ResultScore.GetTopArts().Item1;
            var count = ResultScore.GetTopArts().Item2;
            for (int i = 0; i < 3; i++)
            {
                DebugLogger.Log(i + "位 " + "id: " + id[i] + " count: " + count[i]);
            }
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            var life = PlayerManager.GetManager.GetPlObj.GetComponent<Life>();
            life.Damage(999999999);
        }
    }
}
#endif