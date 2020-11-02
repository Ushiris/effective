using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObjectDelete : MonoBehaviour
{
    public string effectObject_tag = "EffectObject";
    private void OnCollisionEnter(Collision collision)// 何かに当たった瞬間
    {
        if(collision.gameObject.tag == effectObject_tag)// エフェクトオブジェクトの場合
        {
            Destroy(collision.gameObject);

            //DebugLogger.Log("取得！");
        }
    }
}
