using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultScorePresenter : MonoBehaviour
{
    [SerializeField] TitleMenuSelectIcon[] sceneChangeManager;
    [SerializeField] ResultMenuTopArts topArts;
    [SerializeField] ResultMenuScore lvAndBosskill;
    [SerializeField] ResultMenuDegree degree;

    [SerializeField] TextMeshProUGUI endCallText;

    int scoreRollPlayNum = 0;
    bool isMoveEnd;

    int[] lvAndBosskillScore;
    string[] topArtsIdArr;
    string degreeName;

    public delegate void Action();
    public Action OnNext;

    /// <summary>
    /// パネルの枚数を取得
    /// </summary>
    public int GetScorePanelMaxCount => kScorePanelMaxCount;

    static readonly int kScorePanelMaxCount = 3;

    private void Start()
    {
        endCallText.enabled = false;

        //スコア情報の取得
        topArtsIdArr = new string[ResultScore.GetTopArts().Item1.Length];
        for(int i=0; i< topArtsIdArr.Length; i++)
        {
            topArtsIdArr[i] = ResultScore.GetTopArts().Item1[i];
            if (topArtsIdArr[i] == null) topArtsIdArr[i] = "";
        }
        lvAndBosskillScore =
            new int[] { ResultScore.GetWorldLevel, ResultScore.GetBossKillCount };
        degreeName = ResultScore.GetRankName();

        //初期演出
        OnScoreAction(scoreRollPlayNum);
    }

    private void Update()
    {
        if (TitleMenuSelectIcon.IsSceneLoadProcess) return;

        if (!isMoveEnd)
        {
            if (!IsScoreAction(scoreRollPlayNum))
            {
                if (kScorePanelMaxCount - 1 != scoreRollPlayNum)
                {
                    //アニメーションを実行する
                    scoreRollPlayNum++;
                    OnScoreAction(scoreRollPlayNum);
                    OnNextUi();
                }
                else
                {
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
                for (int i = 0; i < sceneChangeManager.Length; i++)
                {
                    sceneChangeManager[i].OnSceneChange();
                }
            }
        }
    }

    //カメラを動かすためのやつ
    void OnNextUi()
    {
        OnNext();
    }

    //アニメーションの実行
    void OnScoreAction(int count)
    {
        switch (count)
        {
            case 0: StartCoroutine(topArts.OnMove(topArtsIdArr)); break;
            case 1: StartCoroutine(lvAndBosskill.OnMove(lvAndBosskillScore)); break;
            case 2: StartCoroutine(degree.OnMove(degreeName)); break;
            default: break;
        }
    }

    //アニメーションが実行されているかどうか
    bool IsScoreAction(int count)
    {
        switch (count)
        {
            case 0: return topArts.isMove;
            case 1: return lvAndBosskill.isMove;
            case 2: return degree.isMove;
            default: return true;
        }
    }

    //アニメーション再生中のパネルの位置
    public Transform GetScorePanelTransform(int num)
    {
        switch (num)
        {
            case 0: return topArts.gameObject.transform;
            case 1: return lvAndBosskill.gameObject.transform;
            case 2: return degree.gameObject.transform;
            default: return null;
        }
    }

    //強制処理
    public void ForcedScoreRollPlay(int num)
    {
        endCallText.enabled = true;
        isMoveEnd = true;
    }
}
