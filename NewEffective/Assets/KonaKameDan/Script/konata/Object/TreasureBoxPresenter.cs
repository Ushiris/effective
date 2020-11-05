using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 宝箱のクラス
/// </summary>
public class TreasureBoxPresenter : ObjStatus
{
    [SerializeField] bool isDebug;
    bool isDeathPlay;

    // Start is called before the first frame update
    void Start()
    {
        SetUp(gameObject);
        SetBreakParticle();
        SetEffect();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDeath() && !isDeathPlay|| isDebug)
        {
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
}
