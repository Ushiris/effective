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

    public void Start()
    {
        requesBGMArrNumber_Old = (int)BGM_PlayBack.RequesBgmFadeArrNumber;
    }
    public void Update()
    {
        if(requesBGMArrNumber_Old != (int)BGM_PlayBack.RequesBgmFadeArrNumber)// BGMが変えられると実行
        {
            requesBGMArrNumber_Old = -1;
            Debug.Log("fadeStop : " + fadeStop);
            fadeStop = BGM_PlayBack.OnFadeVolume(fadeInSpead, fadeOutSpead);
            if (!fadeStop)
                requesBGMArrNumber_Old = (int)BGM_PlayBack.RequesBgmFadeArrNumber;
        }
    }
}
