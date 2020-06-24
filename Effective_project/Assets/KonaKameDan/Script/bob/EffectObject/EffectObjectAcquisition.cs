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
        public int id;
    }
    private EffectObjectID effectObjectID;
    public static List<EffectObjectClass> effectObjectAcquisition = new List<EffectObjectClass>();// リスト作成
    public static List<string> effectObjectName = new List<string>();// リスト作成

    private void Awake()
    {
        //初期化
        if (MainGameManager.GetArtsReset)
        {
            if (effectObjectAcquisition != null) effectObjectAcquisition.Clear();
            if (effectObjectName != null) effectObjectName.Clear();
        }
    }

    private void OnTriggerEnter(Collider other)// 何かに当たった瞬間
    {
        if (other.gameObject.GetComponent<EffectObjectID>() == null) return;

        GameObject anotherObject = other.gameObject;
        effectObjectID = anotherObject.GetComponent<EffectObjectID>();

        AddEffect(effectObjectID.effectObjectType);
    }

    void AddEffect(NameDefinition.EffectName type)
    {
        var effectName = EffectObjectID.effectDictionary[type];
        if (effectObjectName.Contains(effectName))
            effectObjectAcquisition[effectObjectName.IndexOf(effectName)].count++;// ストック追加
        else
        {
            effectObjectAcquisition.Add(new EffectObjectClass { name = effectName, count = 1 });// リストに作成

            //IDの追加
            effectObjectAcquisition[effectObjectAcquisition.Count - 1].id = (int)type;

            effectObjectName.Add(effectName);
        }
    }

    /// <summary>
    /// 所持しているエフェクト取得用
    /// </summary>
    public static List<EffectObjectClass> GetEffectList
    {
        get { return new List<EffectObjectClass>(effectObjectAcquisition); }
    }
}
