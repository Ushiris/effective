using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultScoreInput : MonoBehaviour
{
    // 入れ物
    public TextMeshProUGUI totalDamage;         // 合計ダメージ
    public TextMeshProUGUI playerLevel;         // プレイヤーレベル
    public TextMeshProUGUI playTime;            // プレイタイム
    public TextMeshProUGUI arrivalHierarchy;    // 到達階層
    public TextMeshProUGUI totalEnemiesKilled;  // 倒した敵の数

    // 数値
    private int number_TD;  // 合計ダメージ
    private int number_PL;  // プレイヤーレベル
    private int number_PT;  // プレイタイム
    private int number_AH;  // 到達階層
    private int number_TEK; // 倒した敵の数

    void Start()
    {
        number_TD = MainGameManager.GetTotalDamage;
        number_PL = MainGameManager.GetPlayerLevel;
        number_PT = MainGameManager.GetPlayTime;
        number_AH = MainGameManager.GetArrivalHierarchy;
        number_TEK = MainGameManager.GetTotalEnemiesKilled;
        
        InputDisplay();
    }

    /// <summary>
    /// 値の書き込み
    /// </summary>
    private void InputDisplay()
    {
        totalDamage.text = number_TD.ToString();
        playerLevel.text = number_PL.ToString();
        playTime.text = number_PT.ToString();
        arrivalHierarchy.text = number_AH.ToString();
        totalEnemiesKilled.text = number_TEK.ToString();
    }
}
