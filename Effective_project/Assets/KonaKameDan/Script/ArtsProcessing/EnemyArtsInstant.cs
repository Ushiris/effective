using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 一定の秒数ごとにArtsを放つ
/// </summary>
[RequireComponent(typeof(MyEffectCount))]
[RequireComponent(typeof(EnemyArtsPickUp))]
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
    EnemyArtsPickUp effectPicKUp;
    EnemyBrainBase move;

    private void Awake()
    {
        move = GetComponent<EnemyBrainBase>();
        effectPicKUp = GetComponent<EnemyArtsPickUp>();
        myEffectCount = GetComponent<MyEffectCount>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (myEffectCount != null) SetEffectCount();

        //一定の秒数ごとにArtsを放つ
        timer = gameObject.AddComponent<StopWatch>();
        timer.LapTime = coolTime + Random.Range(0f, 0.5f);
        timer.LapEvent = Action;
    }

    public void ChangeAction(StopWatch.TimeEvent act)
    {
        timer.LapEvent = act;
    }

    /// <summary>
    /// 自分で出すArtsを決めることができるArtsを出す処理
    /// </summary>
    /// <param name="idName"></param>
    public void OnSelfIdSetAction(string idName)
    {
        ArtsInstantManager.InstantArts(artsObj, idName);
    }

    /// <summary>
    /// ピックアップされたArtsIDを返す
    /// </summary>
    public string GetPicKUpArtsId
    {
        get
        {
            return effectPicKUp.GetArtsId;
        }
    }

    //エフェクトを出す処理
    void Action()
    {
        if (!move.IsAttackable()) return;

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
