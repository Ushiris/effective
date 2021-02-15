using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// インゲームのマネージャー
/// </summary>
public class MainGameManager : MonoBehaviour
{
    /// <summary>
    /// 所持しているアーツをリセットするかどうか
    /// </summary>
    public static bool GetArtsReset { get; set; } = true;

    /// <summary>
    /// 合計ダメージを取得
    /// </summary>
    public static int GetTotalDamage { get; set; }

    /// <summary>
    /// プレイヤーレベルを取得
    /// </summary>
    public static int GetPlayerLevel { get; set; }

    /// <summary>
    /// プレイタイムを取得
    /// </summary>
    public static int GetPlayTime { get; set; }

    /// <summary>
    /// 到達階層を取得
    /// </summary>
    public static int GetArrivalHierarchy { get; set; }

    /// <summary>
    /// 倒した敵の数を取得
    /// </summary>
    public static int GetTotalEnemiesKilled { get; set; }

    /// <summary>
    /// 今所持しているエフェクトリストを取得できるぞ！
    /// </summary>
    public static List<EffectObjectAcquisition.EffectObjectClass> GetPlEffectList
    {
        get { return new List<EffectObjectAcquisition.EffectObjectClass>(EffectObjectAcquisition.GetEffectList); }
    }

    private void Start()
    {
        if (GetArtsReset)
        {
            //スコアのリセット 
            ResultPoint.OnResetPoint();
            ResultScore.OnArtsCountReset();
            ResultScore.OnBossKillCountReset();
        }
    }
}
