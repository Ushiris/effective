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

    /// <summary>
    /// 取得したエフェクト一覧
    /// </summary>
    public static EffectObjectAcquisition GetEffectObjAcquisition { get; private set; }

    private void OnTriggerEnter(Collider other)// 何かに当たった瞬間
    {
        if (other.gameObject.GetComponent<EffectObjectID>() == null) return;

        GameObject anotherObject = other.gameObject;
        effectObjectID = anotherObject.GetComponent<EffectObjectID>();

        AddEffect(effectObjectID.effectObjectType);

        //渡す用
        GetEffectObjAcquisition = this;
    }

    void AddEffect(EffectObjectID.EffectObjectType type)
    {
        var effectName = EffectObjectID.effectDictionary[type];
        if (effectObjectName.Contains(effectName))
            effectObjectAcquisition[effectObjectName.IndexOf(effectName)].count++;// ストック追加
        else
        {
            effectObjectAcquisition.Add(new EffectObjectClass { name = effectName, count = 1 });// リストに作成
            effectObjectName.Add(effectName);
        }
    }
}
