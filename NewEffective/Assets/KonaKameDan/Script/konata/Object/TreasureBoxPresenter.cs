using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 宝箱のクラス
/// </summary>
public class TreasureBoxPresenter : ObjStatus
{
    [SerializeField] int maxEffectCount;
    [SerializeField] bool isDebug;
    bool isDeathPlay;

    // Start is called before the first frame update
    void Start()
    {
        SetUp(gameObject);
        SetBreakParticle();
        SetEffect(maxEffectCount);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDeath() && !isDeathPlay|| isDebug)
        {
            PlaySe();
            PlayBreakParticle();
            PlayEffectPurge();
            Destroy(gameObject);
            isDeathPlay = true;
            isDebug = false;
        }
        else
        {
            SetHpBarControl();
        }
    }

    void PlaySe()
    {
        var ran = Random.Range(0, 1);
        if (ran == 0)
        {
            SE_Manager.SePlay(SE_Manager.SE_NAME.ObjCrash1);
        }
        else
        {
            SE_Manager.SePlay(SE_Manager.SE_NAME.ObjCrash2);
        }
    }
}
