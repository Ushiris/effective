﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 設定されたArtsからランダムピックする
/// </summary>
public class EnemyArtsPickUp : MonoBehaviour
{
    /// <summary>
    /// アーツIdの取得
    /// </summary>
    public string GetArtsId { get; private set; }

    public List<NameDefinition.EffectName> GetEffect { get; private set; } = new List<NameDefinition.EffectName>();

    [SerializeField] NameDefinition.EffectName mainType;
    [SerializeField] LvArtsTable[] lvLearnArtsTable;

    int lv = 0;
    int lvLearnArtsTableCount;
    List<string> artsTable = new List<string>();

    [System.Serializable]
    class LvArtsTable
    {
        public int lv;
        public string[] artsArr;
        //public bool isLongDistanceAttack; 離れて攻撃することができるかどうかに使う
    }

    private void Start()
    {
        //レベルアップしたらArtsを新しく覚える
        if (lv != WorldLevel.GetWorldLevel)
        {
            for (; lvLearnArtsTable[lv].lv <= WorldLevel.GetWorldLevel; lv++)
            {
                for (int artsCount = 0; artsCount < lvLearnArtsTable[lv].artsArr.Length; artsCount++)
                {
                    artsTable.Add(lvLearnArtsTable[lv].artsArr[artsCount]);
                }
                if (lvLearnArtsTable.Length - 1 == lv) break;
            }
        }

        //アーツをランダムにピック
        GetArtsId = artsTable[Random.Range(0, artsTable.Count)];

        //アーツIDからEffect情報に変換
        GetEffect.Clear();
        for (int i = 0; i < GetArtsId.Length; i++)
        {
            var num = int.Parse(GetArtsId[i].ToString());
            GetEffect.Add((NameDefinition.EffectName)num);
        }
        GetEffect.Add(mainType);
    }
}
