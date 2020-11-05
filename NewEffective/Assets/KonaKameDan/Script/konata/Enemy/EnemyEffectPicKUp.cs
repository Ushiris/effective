using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffectPicKUp : MonoBehaviour
{
    /// <summary>
    /// アーツIdの取得
    /// </summary>
    public string GetArtsId { get; private set; }

    /// <summary>
    /// メインエフェクトの取得
    /// </summary>
    public NameDefinition.EffectName GetMainEffect { get; private set; }

    /// <summary>
    /// サブエフェクトの取得
    /// </summary>
    public NameDefinition.EffectName[] GetSubEffect { get; private set; }

    [SerializeField] NameDefinition.EffectName mainType;
    [SerializeField] EffectCheck[] plusType = new EffectCheck[(int)NameDefinition.EffectName.Nothing + 1];

    [SerializeField]
    List<NameDefinition.EffectName> effectTable = new List<NameDefinition.EffectName>();

    const int maxLengthId = 2;

    void Reset()
    {
        //インスペクター上に出すときの初期化
        plusType = new EffectCheck[(int)NameDefinition.EffectName.Nothing + 1];
        for (int i = 0; i < plusType.Length; i++)
        {
            var num = (NameDefinition.EffectName)i;
            plusType[i] = new EffectCheck(NameDefinition.GetEffectJapanName[num], num);
        }

       InstantRandomTable();
    }

    void OnValidate()
    {
        InstantRandomTable();
    }

    void Awake()
    {
        string id = "";
        GetSubEffect = new NameDefinition.EffectName[maxLengthId];
        int count = 0;

        //テーブルからランダムにエフェクトをピックアップする
        for (int i = 0; i < maxLengthId; i++)
        {
            int num = Random.Range(0, effectTable.Count - 1);
            if (effectTable[num] != NameDefinition.EffectName.Nothing)
            {
                id += (int)effectTable[num];
                GetSubEffect[count] = effectTable[num];
                count++;
            }
            effectTable.RemoveAt(num);
        }
        id += (int)mainType;

        //IDに変化（Sort）
        GetArtsId = MySort.strSort(id);
    }

    //チェックを入れたものからランダムテーブル作成
    void InstantRandomTable()
    {
        effectTable.Clear();
        if (plusType != null)
        {
            foreach (var item in plusType)
            {
                if (item.check == true)
                {
                    effectTable.Add(item.effectEnum);
                }
            }
            effectTable.Add(mainType);

            GetMainEffect = mainType;
        }
    }
}

[System.Serializable]
class EffectCheck
{
    [HideInInspector] public string effectName;
    [HideInInspector] public NameDefinition.EffectName effectEnum;
    public bool check;

    public EffectCheck(string effectName, NameDefinition.EffectName effectEnum)
    {
        this.effectName = effectName;
        this.effectEnum = effectEnum;
        this.check = true;
    }
}

