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
        [HideInInspector] public int id;
    }

    [Header("初期値")]
    [SerializeField] List<NameDefinition.EffectName> defaultEffect = new List<NameDefinition.EffectName>();

    EffectObjectID effectObjectID;

    public static List<EffectObjectClass> effectObjectAcquisition = new List<EffectObjectClass>();// リスト作成
    public static List<string> effectObjectName = new List<string>();// リスト作成
    public static bool isDefaultStatusReset { get; set; } = true;

    private void Awake()
    {
        //初期化
        if (MainGameManager.GetArtsReset && isDefaultStatusReset)
        {
            if (effectObjectAcquisition != null)
            {
                effectObjectAcquisition.Clear();
            }
            if (effectObjectName != null)
            {
                effectObjectName.Clear();
            }

            //初期値のセット
            foreach (var effect in defaultEffect)
            {
                var name = EffectObjectID.effectDictionary[effect];
                effectObjectAcquisition.Add(new EffectObjectClass
                {
                    name = name,
                    count = 1,
                    id = (int)effect
                });
                effectObjectName.Add(name);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)// 何かに当たった瞬間
    {
        if (collision.gameObject.GetComponent<EffectObjectID>() == null) return;

        GameObject anotherObject = collision.gameObject;
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
