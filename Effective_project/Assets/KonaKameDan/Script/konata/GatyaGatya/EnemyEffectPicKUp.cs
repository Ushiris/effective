using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffectPicKUp : MonoBehaviour
{
    /// <summary>
    /// アーツIdの取得
    /// </summary>
    public string GetArtsId { get; private set; }

    [SerializeField] NameDefinition.EffectName defaultType;
    [SerializeField] EffectCheck[] plusType = new EffectCheck[(int)NameDefinition.EffectName.Nothing + 1];

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
    }

    void OnValidate()
    {
        //チェックを入れたものからランダムテーブル作成
        if (plusType != null)
        {
            foreach (var item in plusType)
            {
                if (item.check == true)
                {
                    effectTable.Add(item.effectEnum);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        string id = "";

        //テーブルからランダムにエフェクトをピックアップする
        for (int i = 0; i < maxLengthId; i++)
        {
            int num = Random.Range(0, effectTable.Count - 1);
            if (effectTable[num] != NameDefinition.EffectName.Nothing)
            {
                id += (int)effectTable[num];
            }
            effectTable.RemoveAt(num);
        }
        id += (int)defaultType;

        //IDに変化（Sort）
        GetArtsId = MySort.strSort(id);
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
        this.check = false;
    }
}

