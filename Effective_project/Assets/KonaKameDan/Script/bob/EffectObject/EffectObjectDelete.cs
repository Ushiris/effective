﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObjectDelete : MonoBehaviour
{
    public string effectObject_tag = "EffectObject";
    private void OnTriggerEnter(Collider other)// 何かに当たった瞬間
    {
        if(other.gameObject.tag == effectObject_tag)// エフェクトオブジェクトの場合
        {
            Destroy(other.gameObject);

            //DebugLogger.Log("取得！");
        }
    }
}
