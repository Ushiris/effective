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

    public UI_CoolTime[] uI_CoolTime;
    static ArtsCoolTime GetMy;

    private void Start()
    {
        GetMy = this;
    }

    //エフェクト所持数ごとにクールタイムを減らすやつ
    static float CoolTimeDown(string id, MyEffectCount myEffectCount)
    {
        float f = 0, average = 0;
        var aim = GetMy.artsInstantManager.prefabs.GetTable()[id];

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
        return GetMy.artsInstantManager.prefabs.GetTable()[id].coolTime - CoolTimeDown(id, myEffectCount);
    }

    /// <summary>
    /// UIの設定
    /// </summary>
    /// <param name="time">クールタイムの経過時間</param>
    /// <param name="maxTime">クールタイムmax値</param>
    /// <param name="id">ArtsID</param>
    public static void SetCoolTimeUI(float time, float maxTime, string id)
    {
        for (int i = 0; i < MyArtsDeck.GetArtsDeck.Count; i++)
        {
            if (MyArtsDeck.GetArtsDeck[i].id == id)
            {
                GetMy.uI_CoolTime[i].SetCoolTime(time, maxTime);
            }
        }
    }
}
