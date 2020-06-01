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

    // Start is called before the first frame update
    void Start()
    {
        tMPro = GetComponent<TextMeshProUGUI>();
        tMPro.enabled = false;
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
                    tMPro.enabled = true;
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
                tMPro.enabled = false;
                on = false;
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
        timer = 0;
        this.onTrigger = onTrigger;
        this.interval = interval;
        Debug.Log(interval);
    }
}
