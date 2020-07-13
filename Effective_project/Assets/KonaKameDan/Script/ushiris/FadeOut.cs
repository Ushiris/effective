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
        return Instantiate(Resources.Load("UI/FadeSystem") as GameObject).GetComponentInChildren<FadeOut>();
    }

    static public GameObject Summon(Sprite img, float duration, float wait, bool isFadeIn = false)
    {
        var instance = Instantiate(Resources.Load("UI/FadeSystem") as GameObject);
        instance.GetComponentInChildren<Image>().sprite = img;
        instance.GetComponentInChildren<FadeOut>().fadeTime = duration;
        instance.GetComponentInChildren<FadeOut>().fadeStartTime = wait;

        return new GameObject();
    }

    static public GameObject Summon()
    {
        return Instantiate(Resources.Load("UI/FadeSystem") as GameObject);
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

        float alpha = isFadeIn ? fadeTime - fadeStartTime / time : time / fadeTime - fadeStartTime;

        Fade.color = new Color(Fade.color.r, Fade.color.g, Fade.color.b, alpha);
    }
}
