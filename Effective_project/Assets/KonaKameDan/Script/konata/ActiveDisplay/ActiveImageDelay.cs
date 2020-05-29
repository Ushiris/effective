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

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        img.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (onTrigger)
        {
            if (interval < timer)
            {
                if (!on)
                {
                    img.enabled = true;
                    on = true;
                }
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        else
        {
            if (on)
            {
                img.enabled = false;
                on = false;
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
        timer = 0;
        this.onTrigger = onTrigger;
        this.interval = interval;
    }
}
