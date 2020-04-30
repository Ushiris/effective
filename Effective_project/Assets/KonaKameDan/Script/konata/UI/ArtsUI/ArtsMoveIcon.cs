using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// アーツ表示アニメーション
/// </summary>
public class ArtsMoveIcon : MonoBehaviour
{
    [SerializeField] Vector3 fixPos = new Vector3(50, 50, 0);
    [SerializeField] float speedPos = 500f;
    [SerializeField] float speedSiz = 20f;

    public float interval = 1;

    Vector3 startPos, startSiz;
    float stepPos, stepSiz;
    bool onStart;

    void Awake()
    {
        //初めのサイズと位置を保存
        startPos = GetComponent<RectTransform>().localPosition;
        startSiz = GetComponent<RectTransform>().localScale;
    }

    void OnEnable()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //指定した位置、サイズに加工
        GetComponent<RectTransform>().localScale = Vector3.zero;
        GetComponent<RectTransform>().localPosition = startPos + fixPos;

        //初期化
        stepPos = 0;
        stepSiz = 0;

        //〇〇後に実行
        Invoke("StartProcess", interval);
    }

    // Update is called once per frame
    void Update()
    {

        if (onStart)
        {
            //速度関係
            stepPos = speedPos * Time.deltaTime;
            stepSiz = speedSiz * Time.deltaTime;

            //移動処理
            GetComponent<RectTransform>().localPosition = Vector3.MoveTowards(
                GetComponent<RectTransform>().localPosition, startPos, stepPos);

            //サイズ変更処理
            GetComponent<RectTransform>().localScale = Vector3.MoveTowards(
                GetComponent<RectTransform>().localScale, startSiz, stepSiz);

            //指定したサイズ、位置に変更された場合処理を止める
            float disPos = Vector3.Distance(GetComponent<RectTransform>().localPosition, startPos);
            float disSiz = Vector3.Distance(GetComponent<RectTransform>().localScale, startSiz);
            if (disPos == 0 && disSiz == 0)
            {
                onStart = false;
            }
        }
    }

    void StartProcess()
    {
        onStart = true;
    }
}
