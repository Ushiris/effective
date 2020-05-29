using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopWatch : MonoBehaviour
{
    public delegate void TimeEvent();

    public float LifeTime { get; private set; }
    float LapTimer = 0f;

    bool isActive = true;

    public TimeEvent LapEvent { get; set; }

    public float LapTime { get; set; }
    
    void Update()
    {
        if (!isActive) return;

        float delta = Time.deltaTime;
        LifeTime += delta;
        LapTimer += delta;

        if (LapTime < LapTimer)
        {
            LapEvent();
            LapTimer -= LapTime;
        }
    }

    public void Pause(bool pause)
    {
        isActive = pause;
    }
}
