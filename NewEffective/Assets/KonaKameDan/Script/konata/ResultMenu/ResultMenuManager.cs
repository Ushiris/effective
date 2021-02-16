using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// リザルトのマネージャー
/// </summary>
public class ResultMenuManager : MonoBehaviour
{
    [SerializeField] ScorePanelController[] scorePanelControllers;
    [SerializeField] TextMeshProUGUI endCallText;

    int scoreRollPlayNum = 0;
    bool isMoveEnd;

    public delegate void Action();
    public Action OnNext;

    /// <summary>
    /// パネルの枚数を取得
    /// </summary>
    public int GetScorePanelMaxCount => scorePanelControllers.Length;

    // Start is called before the first frame update
    void Start()
    {
        endCallText.enabled = false;
        scorePanelControllers[scoreRollPlayNum].ScoreCountMove();
    }

    // Update is called once per frame
    void Update()
    {
        if (TitleMenuSelectIcon.IsSceneLoadProcess) return;

        var maxCount = scorePanelControllers.Length;
        if (!isMoveEnd)
        {
            if (!scorePanelControllers[scoreRollPlayNum].isScoreCountMove)
            {
                if (maxCount - 1 != scoreRollPlayNum)
                {
                    //スコアアニメーションを実行し、カメラを実行中のパネルへ向ける
                    scoreRollPlayNum++;
                    scorePanelControllers[scoreRollPlayNum].ScoreCountMove();
                    OnNextUi();
                }
                else
                {
                    //最後のパネルを見た場合、タイトルメニューに戻る権利を得る
                    endCallText.enabled = true;
                    isMoveEnd = true;
                }
            }
        }
        else
        {
            //タイトルメニューへ行くための処理
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                scorePanelControllers[scoreRollPlayNum].OnSceneChange();
            }
        }
    }

    //カメラを動かすためのやつ
    void OnNextUi()
    {
        OnNext();
    }

    /// <summary>
    /// スコアパネルのTransformを取得
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public Transform GetScorePanelTransform(int num)
    {
        return scorePanelControllers[num].transform;
    }

    /// <summary>
    /// 手動で次のパネルへ行くことができる様にするやつ
    /// </summary>
    /// <param name="num"></param>
    public void ForcedScoreRollPlay(int num)
    {
        scorePanelControllers[scoreRollPlayNum].ForcedScore();
        scoreRollPlayNum = num;
        scorePanelControllers[num].ScoreCountMove();

        endCallText.enabled = true;
        isMoveEnd = true;
    }


}
