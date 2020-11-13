using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_FadeInOut : MonoBehaviour
{
    [Header("フェードインのスピード")]
    [SerializeField] float fadeInSpead = 0.1f;
    [Header("フェードアウトのスピード")]
    [SerializeField] float fadeOutSpead = 0.2f;

    private int requesBGMArrNumber_Old;// フェードが入る前に流れていたBGM
    private bool fadeStop;// フェードし終えたら実行を止める

    public void Update()
    {
        if(requesBGMArrNumber_Old != (int)BGM_PlayBack.RequesBgmFadeArrNumber)// BGMが変えられると実行
        {
            fadeStop = BGM_PlayBack.OnFadeVolume(fadeInSpead, fadeOutSpead);
            if (!fadeStop)
                requesBGMArrNumber_Old = (int)BGM_PlayBack.RequesBgmFadeArrNumber;
        }
    }
}
