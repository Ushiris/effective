using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopWatch : MonoBehaviour
{
    public delegate void TimeEvent();

    float lifeTime = 0f;
    float LapTimer = 0f;

    public TimeEvent LapEvent { get; set; }

    public float LapTime { get; set; }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime;
        lifeTime += delta;
        LapTimer += delta;

        if (LapTime > LapTimer)
        {
            LapTimer -= LapTime;
        }
    }
}
