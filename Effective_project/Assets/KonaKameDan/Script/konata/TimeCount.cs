using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCount : MonoBehaviour
{
    public float setTime;
    public bool onAlarm { get; private set; }
    public bool isPlay { get; private set; }
    float time;

    // Update is called once per frame
    void Update()
    {
        if (isPlay)
        {
            if (time < setTime)
            {
                time += Time.deltaTime;
            }
            else
            {
                isPlay = false;
                onAlarm = true;
                time = 0;
            }
        }
    }

    //スタートさせる
    public void ResetAndStart()
    {
        onAlarm = false;
        isPlay = true;
    }
}
