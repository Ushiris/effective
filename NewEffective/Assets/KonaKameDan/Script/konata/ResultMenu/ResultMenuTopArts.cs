using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultMenuTopArts : MonoBehaviour
{
    [SerializeField] ArtsIconImage artsIconImage;
    [SerializeField] Image[] images;
    [SerializeField] float speed = 30f;
    [SerializeField] float waitTime = 0.3f;

    public bool isMove { get; private set; }

    static readonly float kBeforeSiz = 3;
    static readonly float kStartWaitTime = 0.5f;
    static readonly float kEndWaitTime = 0.5f;

    private void Start()
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].enabled = false;
        }
    }

    public IEnumerator OnMove(string[] artsId)
    {
        isMove = true;
        yield return new WaitForSeconds(kStartWaitTime);
        yield return StartCoroutine(Move(artsId));
        yield return new WaitForSeconds(kEndWaitTime);
        isMove = false;
    }

    IEnumerator Move(string[] artsId)
    {
        for (int i = 0; i < images.Length; i++)
        {
            yield return new WaitForSeconds(waitTime);

            if (artsId[i] != "" && artsId[i] != null)
            {
                images[i].sprite = artsIconImage.data.GetTable()[artsId[i]].image;
                images[i].enabled = true;
            }

            yield return StartCoroutine(OnMoveSiz(images[i], speed, artsId[i]));
        }
    }

    IEnumerator OnMoveSiz(Image image, float speed ,string artsId)
    {
        var rectTransform = image.gameObject.GetComponent<RectTransform>();
        var afterSiz = rectTransform.localScale;
        rectTransform.localScale = afterSiz * kBeforeSiz;

        var dis = -1f;

        while (dis != 0f)
        {
            //移動
            float step = speed * Time.deltaTime;
            rectTransform.localScale =
                Vector3.MoveTowards(rectTransform.localScale, afterSiz, step);

            //距離のチェック
            dis = Vector3.Distance(rectTransform.localScale, afterSiz);
            Debug.Log(dis);

            if (isMove == false)
            {
                rectTransform.localScale = afterSiz;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
