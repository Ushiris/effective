﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//<summary>
//一定時間毎にLapEventに格納されている関数を実行します。
//</summary>
public class StopWatch : MonoBehaviour
{
    public delegate void TimeEvent();

    public float ActiveTime { get; private set; }
    bool isReactiveFlame;

    public TimeEvent LapEvent { get; set; }

    public float LapTime { get; set; }
    public bool IsActive { get; set; }
    public float LapTimer { get; set; }

    public static StopWatch Summon(float lapTime, TimeEvent act,GameObject parent)
    {
        StopWatch instance = parent.AddComponent<StopWatch>();
        instance.LapTime = lapTime;
        instance.LapEvent = act;

        return instance;
    }

    private void Start()
    {
        isReactiveFlame = true;
        LapTimer = 0f;
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

        if (isReactiveFlame)
        {
            //丁度isActiveがtrueになったフレームのみで呼ばれます
            isReactiveFlame = false;
        }
    }

    public void SetActive(bool active)
    {
        if (IsActive != active && active == true)
        {
            isReactiveFlame = true;
        }
        IsActive = active;
    }
}
