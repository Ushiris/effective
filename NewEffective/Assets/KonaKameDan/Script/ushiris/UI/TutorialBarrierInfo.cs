using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBarrierInfo : MonoBehaviour
{
    public static TutorialBarrierInfo instance;
    [SerializeField] TMPro.TMP_Text infoArea;
    [SerializeField] float fadeStartTime = 5f, fadeTime = 2f;
    [SerializeField] string message;
    StopWatch fader;

    private void Awake()
    {
        if (instance = null)
            instance = this;

        Color color = infoArea.color;
        color.a = 0;
        infoArea.color = color;

        fader = StopWatch.Summon(fadeStartTime + fadeTime, StopWatch.voidAction, gameObject);
        fader.DuringEvent = Fade;
    }

    void Fade()
    {
        if (fader.LapTimer < fadeStartTime) return;

        Color color = infoArea.color;
        color.a = 1 - (fader.LapTimer - fadeStartTime) / fadeTime;
    }

    public void Burrier()
    {
        Color color = infoArea.color;
        color.a = 1;
        infoArea.color = color;
    }
}
