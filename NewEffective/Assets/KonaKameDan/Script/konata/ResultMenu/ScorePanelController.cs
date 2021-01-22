using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// パネルごとのスコアアニメーションを管理する
/// </summary>
public class ScorePanelController : MonoBehaviour
{
    [SerializeField] TitleMenuSelectIcon sceneChangeManager;
    [SerializeField] ScoreManager[] scoreManagers;
    [SerializeField] float scoreCountMoveTime = 2;

    /// <summary>
    /// スコアのアニメーション実行中は真
    /// </summary>
    public bool isScoreCountMove { get; private set; }

    /// <summary>
    /// シーンを変更する
    /// </summary>
    public void OnSceneChange()
    {
        sceneChangeManager.OnSceneChange();
    }

    /// <summary>
    /// スコアアニメーションを実行する
    /// </summary>
    public void ScoreCountMove()
    {
        for (int i = 0; i < scoreManagers.Length; i++)
        {
            //スコアを取得
            var name = scoreManagers[i].GetName();
            var score = ResultPoint.SetPoint[name];

            //スコアアニメーション実行命令を発行
            scoreManagers[i].SetMoveTime(scoreCountMoveTime);
            scoreManagers[i].SetScore(score);
            scoreManagers[i].OnCountMove();
        }
        StartCoroutine(ScoreWaitTime());
    }

    /// <summary>
    /// スコアを強制表示する
    /// </summary>
    public void ForcedScore()
    {
        for (int i = 0; i < scoreManagers.Length; i++)
        {
            scoreManagers[i].ForcedScore();
        }
    }

    //スコアアニメーションが終了待ち用
    IEnumerator ScoreWaitTime()
    {
        isScoreCountMove = true;

        for (int i = 0; i < scoreManagers.Length; i++)
        {
            yield return new WaitWhile(() => scoreManagers[i].isCountMove);
        }
        yield return new WaitForSeconds(0.5f);

        isScoreCountMove = false;
    }
}
