using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EffectObjectCount : MonoBehaviour
{
    [SerializeField] private int effectObjectMax = 10;
    public Image[] effectObjectImage;           // エフェクトオブジェクトの入れ物
    public TextMeshProUGUI[] effectObjectName;  // エフェクトオブジェクト名の入れ物

    void Start()
    {
        for (int i = 0; i < effectObjectMax; i++)
        {
            var effectName = EffectObjectID.effectDictionary[(NameDefinition.EffectName)i];


            if (EffectObjectAcquisition.effectObjectName.Contains(effectName))// エフェクトオブジェクトを持っている
            {
                var effectTurn = EffectObjectAcquisition.effectObjectName.FindIndex(s => s == effectName);// 何番目に取得したいエフェクトオブジェクトがあるか
                effectObjectName[i].text = "×" + EffectObjectAcquisition.effectObjectAcquisition[effectTurn].count;// 個数を挿入
            }
            else// エフェクトオブジェクトを持っていない
            {
                effectObjectName[i].text = "×0";// 個数を挿入
            }
        }
    }
}