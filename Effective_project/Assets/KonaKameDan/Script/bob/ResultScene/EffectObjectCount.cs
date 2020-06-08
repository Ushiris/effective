using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectObjectCount : MonoBehaviour
{
    public Image[] effectObjectImage;   // エフェクトオブジェクトの入れ物
    private string[] effectObjectName;  // エフェクトオブジェクトの名前
    private int[] effectObjectCount;    // エフェクトオブジェクトの個数

    void Start()
    {
        for(int i = 0;i < EffectObjectAcquisition.effectObjectAcquisition.Count; i++)
        {
            effectObjectName[i] = EffectObjectAcquisition.effectObjectAcquisition[i].name;  // 名前を挿入
            effectObjectCount[i] = EffectObjectAcquisition.effectObjectAcquisition[i].count;// 個数を挿入
        }
    }
}
