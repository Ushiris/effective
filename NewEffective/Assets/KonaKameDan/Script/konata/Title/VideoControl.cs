using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoControl : MonoBehaviour
{
    [SerializeField] float changeTime = 5f;
    [SerializeField] float fadeOutSpeed = 0.5f;
    [SerializeField] Image blackScreen;
    [SerializeField] VideoPlayer videoPlayer;
    //[SerializeField] VideoClip[] videoClips;

    float time;
    public bool isVideoPlay { get; private set; } = false;

    static readonly Color kDefaultColor = new Color(0, 0, 0, 0);
    static readonly float kAlpha = 1;


    // Start is called before the first frame update
    void Start()
    {
        blackScreen.color = kDefaultColor;
        videoPlayer.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isVideoPlay)
        {
            var color = blackScreen.color;

            if (!videoPlayer.isPlaying)
            {
                if (time != 0)
                {
                    if (color.a != kAlpha)
                    {
                        //フェードアウト
                        color.a += fadeOutSpeed * Time.deltaTime;
                        color.a = Mathf.Clamp(color.a, 0, kAlpha);
                        blackScreen.color = color;
                    }
                    else
                    {
                        //動画再生
                        videoPlayer.time = 0;
                        videoPlayer.gameObject.SetActive(true);
                        videoPlayer.Play();

                    }
                }
                else
                {
                    //動画の再生終了時
                    videoPlayer.gameObject.SetActive(false);
                    isVideoPlay = false;
                }
            }
            else if (videoPlayer.time > 0.5f)
            {
                //動画が流れ始めたら初期化をしていく
                blackScreen.color = kDefaultColor;
                time = 0;

                if (Input.anyKeyDown)
                {
                    videoPlayer.Stop();
                }
            }

        }
        else
        {
            //待つ処理
            if (time < changeTime)
            {
                time += Time.deltaTime;
            }
            else
            {
                isVideoPlay = true;
            }
        }
    }
}
