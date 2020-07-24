using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クールタイム
/// </summary>
public class ArtsCoolTime : MonoBehaviour
{
    //[SerializeField] ArtsInstantManager artsInstantManager;
    public ArtsInstantManager artsInstantManager;
    static ArtsInstantManager artsStatusData;

    private void Start()
    {
        artsStatusData = artsInstantManager;
    }

    //エフェクト所持数ごとにクールタイムを減らすやつ
    static float CoolTimeDown(string id, MyEffectCount myEffectCount)
    {
        float f = 0, average = 0;
        var aim = artsStatusData.prefabs.GetTable()[id];

        foreach (var effect in aim.effect)
        {
            int i = myEffectCount.effectCount[effect];
            average += i;
        }
        f = (average / (float)aim.effect.Count) * aim.coolTimeDown;
        return f;
    }

    /// <summary>
    /// クールタイムを取得する
    /// </summary>
    /// <param name="id"></param>
    /// <param name="myEffectCount"></param>
    /// <returns></returns>
    public static float GetCoolTime(string id, MyEffectCount myEffectCount)
    {
        return CoolTimeDown(id, myEffectCount) + artsStatusData.prefabs.GetTable()[id].coolTime;
    }
}
