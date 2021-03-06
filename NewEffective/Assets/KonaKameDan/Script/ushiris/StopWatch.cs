﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///一定時間毎にLapEventに格納されている関数を実行します。
///</summary>
public class StopWatch : MonoBehaviour
{
    public delegate void TimeEvent();
    public static TimeEvent voidAction = () => { };

    public float ActiveTime { get; private set; }
    bool isReactiveFlame;
    bool isOneShot = false;

    public TimeEvent LapEvent { get; set; }
    public TimeEvent DuringEvent { get; set; } = () => { };

    public float LapTime { get; set; }
    public bool IsActive { get; set; } = true;
    public float LapTimer { get; set; }

    public static StopWatch Summon(float lapTime, TimeEvent act, GameObject parent, bool isOneShot = false)
    {
        StopWatch instance = parent.AddComponent<StopWatch>();
        instance.LapTime = lapTime;
        instance.LapEvent = act;
        instance.isOneShot = isOneShot;
        instance.IsActive = true;

        return instance;
    }

    private void Start()
    {
        isReactiveFlame = true;
        LapTimer = 0f;
    }

    void Update()
    {
        if (!IsActive) return;

        float delta = isReactiveFlame ? 0f : Time.deltaTime;
        ActiveTime += delta;
        LapTimer += delta;

        if (LapTime < LapTimer)
        {
            LapEvent();
            if (isOneShot) Destroy(this);
            LapTimer -= LapTime;
        }

        DuringEvent();

        if (isReactiveFlame)
        {
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

    public void ResetTimer()
    {
        LapTimer = 0f;
        isReactiveFlame = true;
    }
}
