using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    protected delegate void Action();
    protected Action updata;
    protected Action LastStartUp { get; private set; }

    List<GameObject> splitTextObj = new List<GameObject>();
    GameObject group;

    static readonly float kTextCharacterSiz = 0.1f;
    static readonly int kTextFontSiz = 100;
    static readonly TextAnchor kTextAnchor = TextAnchor.MiddleCenter;
    static readonly TextAlignment kTextAlignment = TextAlignment.Center; 

    /// <summary>
    /// 一番初めに呼ぶこと
    /// </summary>
    protected void StartUp()
    {
        group = new GameObject("TextGroup");
        SetLocalPosResetInParent(group, gameObject);
        updata =()=> {  };
    }

    /// <summary>
    /// 文字を出す
    /// </summary>
    /// <param name="text"></param>
    protected void InstantSplitTest(string text)
    {
        foreach(var str in text)
        {
            var word = str.ToString();
            var obj = new GameObject(word);
            var textMesh = obj.AddComponent<TextMesh>();
            SetTextCenter(textMesh);
            textMesh.text = word;

            SetLocalPosResetInParent(obj, group);
            splitTextObj.Add(obj);
        }
    }

    /// <summary>
    ///　中央に文字を並べる
    /// </summary>
    /// <param name="space"></param>
    protected void SetTextObjCenter(float space = 0.8f)
    {
        var maxCount = splitTextObj.Count;
        var dir = (maxCount / 2f + 0.5f) - maxCount;

        for (int i = 0; i < maxCount; i++)
        {
            var pos = splitTextObj[i].transform.localPosition;
            pos.x = (dir + i) * space;
            splitTextObj[i].transform.localPosition = pos;
        }
    }

    /// <summary>
    /// 階段状に文字を動かす処理
    /// </summary>
    /// <param name="speed">速度</param>
    /// <param name="delay">次の文字が動くまで待つ</param>
    /// <param name="againMoveTime">再度動くまでの時間</param>
    /// <param name="minH">下がる高さ</param>
    /// <param name="maxH">上がる高さ</param>
    protected void OnTextWavePlaySetUp(float speed, float delay = 0.2f, float againMoveTime = 3f, float minH = 0f, float maxH = 1.2f)
    {
        int count = 0;
        LastStartUp += () =>
        {
            foreach(var obj in splitTextObj)
            {
                var text = obj.GetComponent<TextMesh>();
                Move(obj, delay * count, againMoveTime - (delay * count));
                if (text.text != " ") count++;
            }
        };

        void Move(GameObject obj, float getDelay, float getAgainMoveTime)
        {
            Vector3 pos = obj.transform.localPosition;
            Vector3 target = new Vector3(pos.x, maxH, pos.y);
            float timer = 0;
            bool isAgainMove = false;

            updata += () =>
            {
                //遅延処理
                if (timer < getDelay)
                {
                    timer += Time.deltaTime;
                    return;
                }
                if (isAgainMove)
                {
                    timer = 0;
                    isAgainMove = false;
                    return;
                }

                //移動
                pos = Vector3.MoveTowards(pos, target, speed * Time.deltaTime);
                if (pos.y == target.y)
                {
                    if (target.y == maxH) target.y = minH;
                    else
                    {
                        //再度動くまでgetAgainMoveTime時間待つ
                        timer -= getAgainMoveTime;
                        isAgainMove = true;
                        target.y = maxH;
                    }
                }
                obj.transform.localPosition = pos;
            };
        }
    }

    //TextMeshの初期化
    void SetTextCenter(TextMesh textMesh)
    {
        textMesh.anchor = kTextAnchor;
        textMesh.alignment = kTextAlignment;
        textMesh.characterSize = kTextCharacterSiz;
        textMesh.fontSize = kTextFontSiz;
    }

    //親子にしたときの座標のズレを正す処理
    void SetLocalPosResetInParent(GameObject obj,GameObject parent)
    {
        obj.transform.parent = parent.transform;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
