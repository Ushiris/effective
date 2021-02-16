using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultMenuScore : MonoBehaviour
{
    [SerializeField] TextMesh[] textMeshes;
    [SerializeField] float time = 2f;
    [SerializeField] float waitTime = 0.3f;

    public bool isMove { get; private set; }

    static readonly float kStartWaitTime = 0.5f;
    static readonly float kEndWaitTime = 0.5f;

    public IEnumerator OnMove(int[] score)
    {
        isMove = true;
        yield return new WaitForSeconds(kStartWaitTime);
        for (int i = 0; i < textMeshes.Length; i++)
        {
            yield return StartCoroutine(OnScoreMove(textMeshes[i], time, score[i]));
        }
        yield return new WaitForSeconds(kEndWaitTime);
        isMove = false;
    }

    IEnumerator OnScoreMove(TextMesh text, float time, int score)
    {
        var v = score / time;
        var a = (v - 0) / time;
        var count = -1;

        yield return new WaitForSeconds(waitTime);

        while (count != score)
        {
            //スコアアニメーション処理
            time += Time.deltaTime * Time.deltaTime;
            count += Mathf.CeilToInt(time * a);
            count = Mathf.Clamp(count, 0, score);
            text.text = string.Format("{0}", count);

            if (isMove == false)
            {
                count = score;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
