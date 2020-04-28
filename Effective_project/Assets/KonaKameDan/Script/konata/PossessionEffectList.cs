using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所持しているエフェクト
/// </summary>
public class PossessionEffectList : MonoBehaviour
{
    [System.Serializable]
    public class PossessionEffect
    {
        public bool flag;
    }
    public PossessionEffect[] possessionEffect = new PossessionEffect[10];

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
}
