using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtsPlateEffectIcon : MonoBehaviour
{
    [System.Serializable]
    public class Icon
    {
        public Sprite image;
    }

    public PrefabDictionary data;
    public delegate void Action();
    public Action updata;

    public bool GetIsCheckMove => isChangeMove;

    bool isChangeMove = true;
    Image image;
    Vector3 tmpPos;
    Vector3 tmpSiz;
    RectTransform rectTransform;

    static readonly Vector3 kStartPos = new Vector3(5, 5, 0);
    static readonly Vector3 kStartSiz = new Vector3(3, 3, 3);

    private void Awake()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        tmpPos = rectTransform.localPosition;
        tmpSiz = rectTransform.localScale;

        updata = () => { };
    }

    /// <summary>
    /// 表示するエフェクト番号入れる
    /// </summary>
    /// <param name="id"></param>
    public void ImageChange(string id)
    {
        if (id == "" || id == null)
        {
            image.enabled = false;
        }
        else
        {
            image.enabled = true;
            image.sprite = data.GetTable()[id].image;
        }
    }

    /// <summary>
    /// アイコンを移動させる
    /// </summary>
    public void ChangeMove(float speed)
    {
        bool isChangeMoveStart = true;
        isChangeMove = true;

        updata = () =>
        {
            if (!isChangeMove) return;

            //ポジション初期化
            if (isChangeMoveStart)
            {
                rectTransform.localPosition = tmpPos + kStartPos;
                isChangeMoveStart = false;
            }

            //移動
            float step = speed * Time.deltaTime;
            rectTransform.localPosition =
                Vector3.MoveTowards(rectTransform.localPosition, tmpPos, step);

            //距離のチェック
            var dis = Vector3.Distance(rectTransform.localPosition, tmpPos);
            if (dis == 0) isChangeMove = false;
        };
    }

    /// <summary>
    /// アイコンのサイズを変える
    /// </summary>
    public void ChangeMoveSiz(float speed)
    {
        bool isChangeMoveStart = true;
        isChangeMove = true;

        updata = () =>
        {
            if (!isChangeMove) return;

            //ポジション初期化
            if (isChangeMoveStart)
            {
                rectTransform.localScale = tmpSiz + kStartSiz;
                isChangeMoveStart = false;
            }

            //移動
            float step = speed * Time.deltaTime;
            rectTransform.localScale =
                Vector3.MoveTowards(rectTransform.localScale, tmpSiz, step);

            //距離のチェック
            var dis = Vector3.Distance(rectTransform.localScale, tmpSiz);
            if (dis == 0) isChangeMove = false;
        };
    }

    [System.Serializable]
    public class PrefabDictionary : Serialize.TableBase<string, Icon, Name2Prefab> { }

    [System.Serializable]
    public class Name2Prefab : Serialize.KeyAndValue<string, Icon>
    {
        public Name2Prefab(string key, Icon value) : base(key, value) { }
    }

}
