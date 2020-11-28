using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// テキストの表示を遅らせる
/// </summary>
public class ActiveTextMeshProDelay : MonoBehaviour
{
    float timer;
    float interval;
    bool on;
    bool onTrigger;

    TextMeshProUGUI tMPro;

    public delegate void OnPlaySe();
    public OnPlaySe onPlaySe;

    // Start is called before the first frame update
    void Start()
    {
        tMPro = GetComponent<TextMeshProUGUI>();
        tMPro.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (onTrigger && !tMPro.enabled)
        {
            if (interval < timer)
            {
                tMPro.enabled = true;
                onPlaySe();
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }

    /// <summary>
    ///  表示のトリガーとインターバル
    /// </summary>
    /// <param name="onTrigger"></param>
    /// <param name="interval"></param>
    public void Delay(bool onTrigger, float interval = 0)
    {
        //初期化
        timer = 0;
        tMPro = GetComponent<TextMeshProUGUI>();
        if (tMPro.enabled) tMPro.enabled = false;

        this.onTrigger = onTrigger;
        this.interval = interval;
        //DebugLogger.Log(interval);
    }
}
