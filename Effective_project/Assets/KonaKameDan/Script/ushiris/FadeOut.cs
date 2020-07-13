using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FadeOut : MonoBehaviour
{
    Image Fade;

    public float fadeTime = 4.0f;
    public float fadeStartTime = 0f;
    public Color endColor = Color.black;

    float time;
    bool isFadeIn = false;

    static public FadeOut Summon(bool isFadeIn = false)
    {
        var instance = Instantiate(Resources.Load("UI/FadeSystem") as GameObject).GetComponentInChildren<FadeOut>();
        instance.isFadeIn = isFadeIn;

        return instance;
    }

    static public GameObject Summon(float duration, float wait, Color color, Sprite img = null, bool isFadeIn = false)
    {
        var instance = Instantiate(Resources.Load("UI/FadeSystem") as GameObject);
        instance.GetComponentInChildren<Image>().sprite = img;
        instance.GetComponentInChildren<FadeOut>().fadeTime = duration;
        instance.GetComponentInChildren<FadeOut>().fadeStartTime = wait;
        instance.GetComponentInChildren<FadeOut>().isFadeIn = isFadeIn;

        return instance;
    }

    static public FadeOut Summon()
    {
        return Instantiate(Resources.Load("UI/FadeSystem") as GameObject).GetComponentInChildren<FadeOut>();
    }

    private void Start()
    {
        Fade = GetComponent<Image>();
        Fade.color = Color.clear;
        time = 0;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time <= fadeStartTime) return;

        float alpha = isFadeIn ? fadeTime - fadeStartTime / ((time <= 0) ? 0.001f : time) : time / ((fadeTime - fadeStartTime <= 0) ? 0.001f : fadeTime - fadeStartTime);

        Fade.color = new Color(Fade.color.r, Fade.color.g, Fade.color.b, alpha);
    }
}
