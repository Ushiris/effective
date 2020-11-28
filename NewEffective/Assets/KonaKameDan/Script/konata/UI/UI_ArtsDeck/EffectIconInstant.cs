using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 画像の移動
/// </summary>
public class EffectIconInstant : MonoBehaviour
{
    [SerializeField] Vector3 movePos = new Vector3(5, 5, 0);
    [SerializeField] float speed = 3;
    Vector3 tmpPos;
    Image img;
    RectTransform rectTransform;


    [SerializeField] bool isTestReset;
    [SerializeField] bool isTestMove;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        tmpPos = rectTransform.localPosition;
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (img.enabled) move();
        //else PosReset();
    }

    //ポジションのリセット
    public void PosReset()
    {
        rectTransform.localPosition = tmpPos + movePos;
    }

    //移動
    public void move()
    {
        float step = speed * Time.deltaTime;
        rectTransform.localPosition =
            Vector3.MoveTowards(rectTransform.localPosition, tmpPos, step);
    }

    void MyDebug()
    {
        if (isTestReset)
        {
            PosReset();
            isTestReset = false;
        }
        else if (isTestMove)
        {
            move();
        }
    }
}
