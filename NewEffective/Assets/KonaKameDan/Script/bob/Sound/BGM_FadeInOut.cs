using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_FadeInOut : MonoBehaviour
{
    /// <summary>
    /// フェードしてる？フェードしてない？
    /// </summary>
    public enum BGM_Switch
    {
        Fade, FadeNot
    }
    /// <summary>
    /// フェードイン,フェードアウト
    /// </summary>
    public enum BGM_Fade
    {
        FadeIn, FadeOut
    }
    /// <summary>
    /// BGMの種類名
    /// </summary>
    BGM_Manager.BGM_NAME bgm_Name;
    [Range(0, 1)]
    private float volume;// 最終的なボリューム
    private BGM_Switch fade;// フェードしてるか否か
    [Header("フェードインのスピード")]
    [SerializeField] float fadeInSpead = 0.1f;
    [Header("フェードアウトのスピード")]
    [SerializeField] float fadeOutSpead = 0.2f;

    private int requesBGMArrNumber_Old;
    private bool fadeStop;

    public static float bgmFadeInVolume { get; private set; }

    public void Update()
    {
        if(requesBGMArrNumber_Old != BGM_PlayBack.RequesBgmFadeArrNumber)
        {
            fadeStop = BGM_PlayBack.OnFadeVolume(fadeInSpead, fadeOutSpead);
            if (!fadeStop)
                requesBGMArrNumber_Old = BGM_PlayBack.RequesBgmFadeArrNumber;
        }
    }
}
