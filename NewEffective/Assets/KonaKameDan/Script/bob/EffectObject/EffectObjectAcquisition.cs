﻿using System.Collections;
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

    public MyEffectCount effectBag;
    [Header("初期値")]
    [SerializeField] List<NameDefinition.EffectName> defaultEffect = new List<NameDefinition.EffectName>();

    EffectObjectID effectObjectID;

    public static List<EffectObjectClass> effectObjectAcquisition = new List<EffectObjectClass>();// リスト作成
    public static Dictionary<NameDefinition.EffectName, int> GetEffectBag { get; private set; } = new Dictionary<NameDefinition.EffectName, int>();
    public static List<string> effectObjectName = new List<string>();// リスト作成
    public static List<int> effectObjNum = new List<int>();
    public static bool isDefaultStatusReset { get; set; } = true;

    private void OnValidate()
    {
        GetEffectBag = new Dictionary<NameDefinition.EffectName, int>(effectBag.effectCount);
    }

    private void Awake()
    {
        if (GetEffectBag == null) GetEffectBag = new Dictionary<NameDefinition.EffectName, int>(effectBag.effectCount);

        //初期化
        if (MainGameManager.GetArtsReset && isDefaultStatusReset)
        {
            ResetBag();

            if (effectObjectAcquisition != null)
            {
                effectObjectAcquisition.Clear();
            }
            if (effectObjectName != null)
            {
                effectObjectName.Clear();
                effectObjNum.Clear();
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
                effectObjNum.Add((int)effect);
                GetEffectBag[effect] = 1;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)// 何かに当たった瞬間
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
        {
            effectObjectAcquisition[effectObjectName.IndexOf(effectName)].count++;// ストック追加
        }
        else
        {
            effectObjectAcquisition.Add(new EffectObjectClass { name = effectName, count = 1 });// リストに作成

            //IDの追加
            effectObjectAcquisition[effectObjectAcquisition.Count - 1].id = (int)type;

            effectObjectName.Add(effectName);
            effectObjNum.Add((int)type);
        }

        GetEffectBag[type]++;
    }

    void ResetBag()
    {
        GetEffectBag= new Dictionary<NameDefinition.EffectName, int>()
        {
            {NameDefinition.EffectName.Shot,        0 },
            {NameDefinition.EffectName.Slash,       0 },
            {NameDefinition.EffectName.Barrier,     0 },
            {NameDefinition.EffectName.Trap,        0 },
            {NameDefinition.EffectName.Spread,      0 },
            {NameDefinition.EffectName.Homing,      0 },
            {NameDefinition.EffectName.Drain,       0 },
            {NameDefinition.EffectName.Explosion,   0 },
            {NameDefinition.EffectName.Slow,        0 },
            {NameDefinition.EffectName.Fly,         0 },
        };
    }

    /// <summary>
    /// 所持しているエフェクト取得用
    /// </summary>
    public static List<EffectObjectClass> GetEffectList
    {
        get { return effectObjectAcquisition; }
    }

    /// <summary>
    /// 所持数を好きな数にできる
    /// </summary>
    /// <param name="name"></param>
    /// <param name="num"></param>
    public static void SetEffectBag(NameDefinition.EffectName name, int num)
    {
        GetEffectBag[name] = num;
    }
}
