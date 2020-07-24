using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 一定の秒数ごとにArtsを放つ
/// </summary>
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

    // Start is called before the first frame update
    void Start()
    {
        effectPicKUp = gameObject.GetComponent<EnemyEffectPicKUp>();
        myEffectCount = gameObject.GetComponent<MyEffectCount>();

        SetEffectCount();

        //一定の秒数ごとにArtsを放つ
        timer = gameObject.AddComponent<StopWatch>();
        timer.LapTime = coolTime;
        timer.LapEvent = () => { Action(); };
    }

    //エフェクトを出す処理
    void Action()
    {
        //ArtsID検出
        string id = effectPicKUp.GetArtsId;

        //Artsを放つ
        ArtsInstantManager.InstantArts(artsObj, id);
    }

    //エフェクトの所持数の定義
    void SetEffectCount()
    {
        int count = 0;
        foreach(var key in myEffectCount.effectCount.Keys)
        {
            if (key == effectPicKUp.GetMainEffect)
            {
                myEffectCount.effectCount[key] = mainEffectCount;
            }
            else if(key == effectPicKUp.GetSubEffect[count])
            {
                myEffectCount.effectCount[key] = defaultEffectCount;
                count++;
            }
        }
    }
}
