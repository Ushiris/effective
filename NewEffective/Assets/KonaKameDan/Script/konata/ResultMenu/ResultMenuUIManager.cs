using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultMenuUIManager : MonoBehaviour
{
    [SerializeField] ResultMenuCamera resultMenuCamera;
    [SerializeField] float radius = 10;

    bool isScoreRollPlayEnd;
    ResultMenuUI resultMenuUI;

    public delegate void Action();
    public Action isNext;

    public GameObject[] scoreUi;
    public int scoreRollPlayNum = 0;

    static readonly Vector3 fixPos = new Vector3(332.9f, 62.07f, 223.1f);
    static readonly float kAngle = 360;

    private void Awake()
    {
        for (int i = 0; i < scoreUi.Length; i++)
        {
            float r = (kAngle / scoreUi.Length) * i;
            r *= Mathf.Deg2Rad;

            var pos = new Vector3(radius * Mathf.Cos(r), 0f, radius * Mathf.Sin(r)) + fixPos;
            scoreUi[i] = Instantiate(scoreUi[i], pos, Quaternion.identity);
            scoreUi[i].transform.rotation = Look(fixPos, scoreUi[i].transform.position);

            var point = scoreUi[i].GetComponent<ResultMenuUI>();
            point.SetScore = ResultPoint.GetPoint(i);
        }

        resultMenuUI = scoreUi[scoreRollPlayNum].GetComponent<ResultMenuUI>();
        resultMenuUI.IsPlay = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (TitleMenuSelectIcon.IsSceneLoadProcess) return;

        //タイトルに移動する処理
        if (resultMenuCamera.IsMoveEnd)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                var s = scoreUi[scoreRollPlayNum].GetComponent<TitleMenuSelectIcon>();
                s.OnSceneChange();
            }
        }

        //得点が動ききり、一定の秒数後にCameraを動かす命令を出す
        if (!resultMenuUI.IsPlay)
        {
            if (scoreUi.Length - 1 != scoreRollPlayNum && !isScoreRollPlayEnd)
            {
                scoreRollPlayNum++;
                OnScoreRollPlay();
                OnNextUi();
            }
            else isScoreRollPlayEnd = true;
        }
    }

    Quaternion Look(Vector3 target, Vector3 my)
    {
        var aim = target - my;
        var look = Quaternion.LookRotation(aim);
        return look * Quaternion.AngleAxis(0, Vector3.up);
    }

    void OnNextUi()
    {
        isNext();
    }

    void OnScoreRollPlay()
    {
        resultMenuUI = scoreUi[scoreRollPlayNum].GetComponent<ResultMenuUI>();
        resultMenuUI.IsPlay = true;
    }

    public void ForcedScoreRollPlay(int num)
    {
        scoreRollPlayNum = num;
        OnScoreRollPlay();
    }
}
