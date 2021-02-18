using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ResultScore : MonoBehaviour
{
    static Dictionary<string, int> artsCount = new Dictionary<string, int>();

    //称号の判定ライン//
    static readonly int kInstantDeath = 0;
    static readonly int kLowMax = 31;
    static readonly int kNormalMax = 81;

    //称号//
    //例外
    static readonly string kInstantDeathName = "無垢な子";
    static readonly string kTrinityName = "トリニティー";
    static readonly string kDuoName = "多くを知るもの";
    //アタック
    static readonly string kLowAttackName = "戦闘家";
    static readonly string kNormalAttackName = "狂戦士";
    static readonly string kHighAttackName = "破滅を呼ぶもの";
    //ディフェンス
    static readonly string kLowDefenseName = "守るもの";
    static readonly string kNormalDefenseName = "頑強なる戦士";
    static readonly string kHighDefenseName = "逆境を退くもの";
    //テクニック
    static readonly string kLowTechnicName = "テクニシャン";
    static readonly string kNormalTechnicName = "技巧士";
    static readonly string kHighTechnicName = "技法を伝うもの";
    //エラー
    static readonly string kUnknownName = "不明";


    /// <summary>
    /// ボスを倒した数を取得する
    /// </summary>
    public static int GetBossKillCount { get; private set; }

    /// <summary>
    /// WorldLevelを取得する
    /// </summary>
    public static int GetWorldLevel => WorldLevel.GetWorldLevel;

    /// <summary>
    /// ボスの討伐カウントを増やす
    /// </summary>
    public static void OnBossKillCountPlus()
    {
        GetBossKillCount++;
    }

    /// <summary>
    /// ボス討伐カウントをリセットする
    /// </summary>
    public static void OnBossKillCountReset()
    {
        GetBossKillCount = 0;
    }

    /// <summary>
    /// 称号を取得する
    /// </summary>
    /// <returns></returns>
    public static string GetRankName()
    {
        int attack = 0;
        int defense = 0;
        int technic = 0;

        if (artsCount.Count == kInstantDeath)
        {
            //スコアがない場合
            return kInstantDeathName;
        }

        //使われたArtsからタイプを計算する
        foreach (var item in artsCount)
        {
            switch (NameDefinition.GetArtsType(item.Key))
            {
                case NameDefinition.ArtsType.Attack: attack += item.Value; break;
                case NameDefinition.ArtsType.Defense: defense += item.Value; break;
                case NameDefinition.ArtsType.Technic: technic += item.Value; break;
                default: break;
            }
        }

        DebugLogger.Log("attack: " + attack);

        if (attack == defense && defense == technic)
        {
            //アタック,ディフェンス,テクニックが同じスコアな場合
            return kTrinityName;
        }
        else
        {
            var list = new List<int>() { attack, defense, technic };
            list.Sort();
            list.Reverse();

            var firstPlace = list[0];
            var secondPlace = list[1];

            if (firstPlace == secondPlace)
            {
                //最も多いものとその次に多いものが等しい場合
                return kDuoName;
            }
            else
            {
                //最も多いタイプごとに称号を出す
                var point = firstPlace - secondPlace;
                DebugLogger.Log("point: " + point);
                if (firstPlace == attack)
                {
                    return GetScoreName(kLowAttackName, kNormalAttackName, kHighAttackName, point);
                }
                else if (firstPlace == defense)
                {
                    return GetScoreName(kLowDefenseName, kLowDefenseName, kHighDefenseName, point);
                }
                else if (firstPlace == technic)
                {
                    return GetScoreName(kLowTechnicName, kNormalTechnicName, kHighTechnicName, point);
                }
                else
                {
                    return kUnknownName;
                }
            }
        }

        //ポイントによって評価を出す
        string GetScoreName(string low, string normal, string high, int point)
        {
            if (point < kLowMax)
            {
                return low;
            }
            else if (point < kNormalMax)
            {
                return normal;
            }
            else
            {
                return high;
            }
        }
    }

    /// <summary>
    /// 配列を初期化する
    /// </summary>
    public static void OnArtsCountReset()
    {
        artsCount.Clear();
    }

    /// <summary>
    /// Artsを記録する
    /// </summary>
    /// <param name="id"></param>
    public static void SetArtsCount(string id)
    {
        if (!artsCount.ContainsKey(id))
        {
            if (artsCount.Count == 0)
            {
                artsCount.Add(id, 1);
            }
            else
            {
                artsCount.Add(id, 1);
            }
        }
        else
        {
            artsCount[id]++;
        }
    }

    /// <summary>
    /// 登録させれているものから最も多いものを出す
    /// string = id int = count
    /// </summary>
    /// <param name="maxCount"></param>
    /// <returns></returns>
    public static (string[], int[]) GetTopArts(int maxCount = 3)
    {
        string[] id = new string[maxCount];
        int[] count = new int[maxCount];
        var i = 0;

        var val = artsCount.OrderByDescending((x) => x.Value);
        foreach (var v in val)
        {
            id[i] = v.Key;
            count[i] = v.Value;
            i++;
            if (i == maxCount) break;
        }

        return (id, count);
    }
}
