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
    public TextMeshProUGUI TotalEnemiesKilled;  // 倒した敵の数

    // 数値
    private int number_TD;  // 合計ダメージ
    private int number_PL;  // プレイヤーレベル
    private int number_PT;  // プレイタイム
    private int number_AH;  // 到達階層
    private int number_TEK; // 倒した敵の数

    void Start()
    {
        number_TD = 10;// test
        number_PL = 10;// test
        number_PT = 10;// test
        number_AH = 10;// test
        number_TEK = 10;// test

        InputDisplay();
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// 書き込み部分
    /// </summary>
    private void InputDisplay()
    {
        totalDamage.text = number_TD.ToString();
        playerLevel.text = number_PL.ToString();
        playTime.text = number_PT.ToString();
        arrivalHierarchy.text = number_AH.ToString();
        TotalEnemiesKilled.text = number_TEK.ToString();
    }
}
