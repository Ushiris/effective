using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 一定の秒数ごとにArtsを放つ
/// </summary>
[RequireComponent(typeof(MyEffectCount))]
public class EnemyArtsInstant : MonoBehaviour
{
    [Header("アーツを放つためのステータス")]
    [SerializeField] float coolTime = 3f;
    [SerializeField] GameObject artsObj;

    [Header("エフェクトの所持数定義")]
    [SerializeField] int mainEffectCount = 3;
    [SerializeField] int defaultEffectCount = 1;


    StopWatch timer;
    MyEffectCount myEffectCount;
    EnemyEffectPicKUp effectPicKUp;
    EnemyState move;

    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<EnemyState>();
        effectPicKUp = GetComponent<EnemyEffectPicKUp>();
        myEffectCount = GetComponent<MyEffectCount>();

        if(myEffectCount!=null) SetEffectCount();

        //一定の秒数ごとにArtsを放つ
        timer = gameObject.AddComponent<StopWatch>();
        timer.LapTime = coolTime;
        timer.LapEvent = Action;
    }

    //エフェクトを出す処理
    void Action()
    {
        if (!EnemyState.IsAttackable(move.move)) return;

        //ArtsID検出
        string id = effectPicKUp.GetArtsId;

        //Artsを放つ
        ArtsInstantManager.InstantArts(artsObj, id);
    }

    //エフェクトの所持数の定義
    void SetEffectCount()
    {
        //メインエフェクト
        myEffectCount.effectCount[effectPicKUp.GetMainEffect] = mainEffectCount;

        //サブエフェクト
        foreach (var item in effectPicKUp.GetSubEffect)
        {
            myEffectCount.effectCount[item] = defaultEffectCount;
        }
    }
}
