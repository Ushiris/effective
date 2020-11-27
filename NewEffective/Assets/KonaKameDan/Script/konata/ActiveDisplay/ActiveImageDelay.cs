using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 画像の表示を遅らせる
/// </summary>
public class ActiveImageDelay : MonoBehaviour
{
    float timer;
    float interval;
    bool on;
    bool onTrigger;

    Image img;

    public delegate void OnPlaySe();
    public OnPlaySe onPlaySe;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        img.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (onTrigger && !img.enabled)
        {
            if (interval < timer)
            {
                img.enabled = true;

                //SE
                onPlaySe();
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// 表示のトリガーとインターバル
    /// </summary>
    /// <param name="onTrigger"></param>
    /// <param name="interval"></param>
    public void Delay(bool onTrigger, float interval = 0)
    {
        //初期化
        timer = 0;
        img = GetComponent<Image>();
        if (img.enabled) img.enabled = false;

        this.onTrigger = onTrigger;
        this.interval = interval;
    }
}
