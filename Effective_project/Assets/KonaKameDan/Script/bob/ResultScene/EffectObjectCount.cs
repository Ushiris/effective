using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectObjectCount : MonoBehaviour
{
    public Image[] effectObjectImage;   // エフェクトオブジェクトの入れ物
    private int[] effectObjectCount;    // エフェクトオブジェクトの個数

    void Start()
    {
        for(int i = 0;i < EffectObjectAcquisition.effectObjectAcquisition.Count; i++)
        {
            effectObjectCount[i] = EffectObjectAcquisition.effectObjectAcquisition[i].count;// 個数を挿入
        }
    }
}
