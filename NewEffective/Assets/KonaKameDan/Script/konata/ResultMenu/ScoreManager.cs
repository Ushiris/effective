using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スコアアニメーションの処理
/// </summary>
public class ScoreManager : MonoBehaviour
{
    [SerializeField] ResultPoint.PointName name;
    [SerializeField] float t = 2f;
    [SerializeField] float v0 = 0f;
    [SerializeField] int score;
    [SerializeField] TextMesh text;

    float time;
    float v;
    float a;
    int count;

    /// <summary>
    /// スコアアニメーション実行中は真
    /// </summary>
    public bool isCountMove { get; private set; } = false;

    // Update is called once per frame
    void Update()
    {
        if (!isCountMove) return;

        //スコアアニメーション処理
        time += Time.deltaTime * Time.deltaTime;
        count += (int)(time * a);
        count = Mathf.Clamp(count, 0, score);
        text.text = count.ToString();

        if (count == score) isCountMove = false;
    }

    /// <summary>
    /// スコアアニメーションの実行時間を設定する
    /// </summary>
    /// <param name="time"></param>
    public void SetMoveTime(float time)
    {
        t = time;
    }

    /// <summary>
    /// スコアの設定
    /// </summary>
    /// <param name="score"></param>
    public void SetScore(int score)
    {
        this.score = score;
    }

    /// <summary>
    /// スコアアニメーションを実行させる
    /// </summary>
    public void OnCountMove()
    {
        //加速の計算
        v = score / t;
        a = (v - v0) / t;

        isCountMove = true;
    }

    /// <summary>
    /// スコアの名称を取得
    /// </summary>
    /// <returns></returns>
    public ResultPoint.PointName GetName()
    {
        return name;
    }

    /// <summary>
    /// スコアを強制表示させる
    /// </summary>
    public void ForcedScore()
    {
        isCountMove = false;
        count = score;
        text.text = score.ToString();
    }
}
