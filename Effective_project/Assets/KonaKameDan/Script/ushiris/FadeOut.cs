using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FadeOut : MonoBehaviour
{
    Image Fade;

    public float fadeTime = 2.0f;
    public float fadeStartTime = 0f;
    public Color endColor = Color.black;

    float time;

    static public FadeOut Create(float duration,float wait,Color endColor)
    {
        return new FadeOut {
            fadeTime = duration,
            fadeStartTime = wait,
            endColor = endColor
        };
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
        Fade.color = new Color(Fade.color.r, Fade.color.g, Fade.color.b, time / fadeTime);
    }
}
