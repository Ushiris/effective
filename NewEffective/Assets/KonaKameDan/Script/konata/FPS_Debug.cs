using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Debug : MonoBehaviour
{

    [SerializeField]
    private float updateInterval = 0.1f;

    float accum;
    int frames;
    float timeleft;
    float fps;

    private void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        frames++;

        if (0 < timeleft) return;

        fps = accum / frames;
        timeleft = updateInterval;
        accum = 0;
        frames = 0;
    }

    private void OnGUI()
    {
        GUILayout.Label("FPS: " + fps.ToString("f2"));
    }
}
