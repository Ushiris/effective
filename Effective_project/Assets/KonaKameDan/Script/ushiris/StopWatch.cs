using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//<summary>
//一定時間毎にLapEventに格納されている関数を実行します。
//</summary>
public class StopWatch : MonoBehaviour
{
    public delegate void TimeEvent();

    public float ActiveTime { get; private set; }
    float LapTimer = 0f;

    bool isActive = true;
    bool isReactiveFlame;

    public TimeEvent LapEvent { get; set; }

    public float LapTime { get; set; }

    private void Start()
    {
        isReactiveFlame = true;
    }

    void Update()
    {
        float delta = isReactiveFlame ? 0f : Time.deltaTime;
        ActiveTime += delta;
        LapTimer += delta;

        if (LapTime < LapTimer)
        {
            LapEvent();
            LapTimer -= LapTime;
        }

        if(isReactiveFlame)
        {
            //丁度isActiveがtrueになったフレームのみで呼ばれます
            isReactiveFlame = false;
        }
    }

    public void SetActive(bool active)
    {
        if (isActive != active && active == true)
        {
            isReactiveFlame = true;
        }
        isActive = active;
    }
}
