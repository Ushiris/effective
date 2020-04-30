using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所持しているエフェクト
/// </summary>
public class PossessionEffectList : MonoBehaviour
{
    public enum Name { a, b, c }
    Name EffectName;


    [System.Serializable]
    public class PossessionEffect
    {
        [HideInInspector] public string name;   //所持アイテムの名前
        public bool flag;                       //所持フラグ
        public int count;
    }
    public List<PossessionEffect> possessionEffect = new List<PossessionEffect>()
    {
        new PossessionEffect{name=Name.a.ToString() },
        new PossessionEffect{name=Name.b.ToString() },
        new PossessionEffect{name=Name.c.ToString() }
    };

    static PossessionEffectList GetEffectList;

    // Start is called before the first frame update
    void Awake()
    {
        GetEffectList = this;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    /// <summary>
    /// そのエフェクトを所持しているかどうか
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public static bool GetIsPossessionEffect(int num)
    {
        return GetEffectList.possessionEffect[num].flag;
    }

    public static int GetEffectListCount() { return GetEffectList.possessionEffect.Count; }
}
