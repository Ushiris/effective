using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopWatch : MonoBehaviour
{
    public delegate void TimeEvent();

    public float LifeTime { get; private set; }
    float LapTimer = 0f;

    public TimeEvent LapEvent { get; set; }

    public float LapTime { get; set; }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime;
        LifeTime += delta;
        LapTimer += delta;

        if (LapTime > LapTimer)
        {
            LapEvent();
            LapTimer -= LapTime;
        }
    }
}
