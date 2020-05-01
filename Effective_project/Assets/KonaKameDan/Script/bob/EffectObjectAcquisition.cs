using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObjectAcquisition : MonoBehaviour
{
    private EffectObjectID effectObjectID;
    private void OnTriggerEnter(Collider other)// 何かに当たった瞬間
    {
        GameObject anotherObject = other.gameObject;
        effectObjectID = anotherObject.GetComponent<EffectObjectID>();

        switch (effectObjectID.effectObjectType)// 当たったエフェクトオブジェクトが何かを取得
        {
            case EffectObjectID.EffectObjectType.RED:
                Debug.Log("赤");
                break;

            case EffectObjectID.EffectObjectType.BLUE:
                Debug.Log("青");
                break;

            case EffectObjectID.EffectObjectType.EYLLOW:
                Debug.Log("黄");
                break;

            default:
                Debug.Log("その他");
                break;
        }
    }
}
