using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBarrierInfo : MonoBehaviour
{
    public static TutorialBarrierInfo instance;
    [SerializeField] Text infoArea;
    [SerializeField] float fadeStartTime = 5f, fadeTime = 2f;
    [SerializeField] string message;
    StopWatch fader;

    private void Start()
    {
        if (instance = null)
            instance = this;

        infoArea.text = message;

        Color color = infoArea.color;
        color.a = 0;
        infoArea.color = color;

        fader = StopWatch.Summon(fadeStartTime + fadeTime, StopWatch.voidAction, gameObject);
        fader.DuringEvent = Fade;
        fader.SetActive(false);
    }

    void Fade()
    {
        if (fader.LapTimer <= fadeStartTime) return;

        Color color = infoArea.color;
        color.a = 1 - (fader.LapTimer - fadeStartTime) / fadeTime;
        infoArea.color = color;
    }

    public void Burrier()
    {
        if (fader.IsActive) return;

        fader.SetActive(true);
        fader.ResetTimer();

        Color color = infoArea.color;
        color.a = 1;
        infoArea.color = color;
    }
}
