using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObjectAcquisition : MonoBehaviour
{
    [System.Serializable]
    public class EffectObjectClass
    {
        [HideInInspector] public string name;
        public int count;
    }
    private EffectObjectID effectObjectID;
    public List<EffectObjectClass> effectObjectAcquisition = new List<EffectObjectClass>();// リスト作成
    public List<string> effectObjectName = new List<string>();// リスト作成
    private void OnTriggerEnter(Collider other)// 何かに当たった瞬間
    {
        GameObject anotherObject = other.gameObject;
        effectObjectID = anotherObject.GetComponent<EffectObjectID>();

        switch (effectObjectID.effectObjectType)// 当たったエフェクトオブジェクトが何かを取得
        {
            case EffectObjectID.EffectObjectType.RED:
                if (effectObjectName.Contains("赤"))
                    effectObjectAcquisition[effectObjectName.IndexOf("赤")].count++;// ストック追加
                else
                {
                    effectObjectAcquisition.Add(new EffectObjectClass { name = "赤", count = 1 });// リストに作成
                    effectObjectName.Add("赤");
                }
                break;

            case EffectObjectID.EffectObjectType.BLUE:
                if (effectObjectName.Contains("青"))
                    effectObjectAcquisition[effectObjectName.IndexOf("青")].count++;// ストック追加
                else
                {
                    effectObjectAcquisition.Add(new EffectObjectClass { name = "青", count = 1 });// リストに作成
                    effectObjectName.Add("青");
                }
                break;

            case EffectObjectID.EffectObjectType.EYLLOW:
                if (effectObjectName.Contains("黄"))
                    effectObjectAcquisition[effectObjectName.IndexOf("黄")].count++;// ストック追加
                else
                {
                    effectObjectAcquisition.Add(new EffectObjectClass { name = "黄", count = 1 });// リストに作成
                    effectObjectName.Add("黄");
                }
                break;

            default:
                Debug.Log("その他");
                break;
        }
    }
}
