using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_PlayBack : MonoBehaviour
{
    /// <summary>
    /// BGMのパート分け
    /// </summary>
    public enum BGM_Part
    {
        Start, Loop
    }
    /// <summary>
    /// BGMの種類名
    /// </summary>
    static BGM_Manager.BGM_NAME bgm_Name;
    static AudioSource[] bgm;
    static float volume;
    static bool playBgmStartSwitch;
    static bool playBgmLoopSwitch;
    static BGM_Manager bgm_Manager;

    void Start()
    {
        bgm_Manager = GetComponent<BGM_Manager>();
        bgm = GetComponents<AudioSource>();
    }

    void Update()
    {
        Debug.Log("playBgmStartSwitch : " + playBgmStartSwitch);
        if (playBgmStartSwitch)// 冒頭を再生
        {
            bgm[(int)bgm_Name * 2 + 1].Stop();// 停止
            bgm[(int)bgm_Name * 2].volume = volume;// 音量調整
            bgm[(int)bgm_Name * 2].Play();// 再生
            playBgmStartSwitch = false;
            PlayBGMArrNumber = (int)bgm_Name * 2;
        }

        if((!bgm[(int)bgm_Name * 2].isPlaying && !bgm[(int)bgm_Name * 2 + 1].isPlaying) || playBgmLoopSwitch)// ループを再生
        {
            bgm[(int)bgm_Name * 2 + 1].volume = bgm[(int)bgm_Name * 2].volume;// 音量調整
            bgm[(int)bgm_Name * 2 + 1].Play();// 再生
            PlayBGMArrNumber = (int)bgm_Name * 2 + 1;
        }
    }
    /// <summary>
    /// BGMを再生;
    /// 　BGM_NAME:再生したいBGM・
    /// 　volume:BGMの音量・
    /// 　startOrLoop:「true」で冒頭から、「false」でループから
    /// </summary>
    public static void BgmStartPlayBack(BGM_Manager.BGM_NAME bgmName,float startVolume, bool startOrLoop)
    {
        bgm_Name = bgmName;
        volume = startVolume;
        if(startOrLoop == true)
            playBgmStartSwitch = true;// 冒頭からスイッチ
        else if (startOrLoop == false)
            playBgmLoopSwitch = true;// ループからスイッチ
    }
    public static int PlayBGMArrNumber { get; private set; }
    public static int RequesBgmFadeArrNumber { get; set; }
    public static bool OnFadeVolume (float fadeInVolume, float faeOutVolume)
    {
        if (bgm[RequesBgmFadeArrNumber].volume > bgm_Manager.bgmDataList[RequesBgmFadeArrNumber / 2].bgmVolume)
        {
            bgm[RequesBgmFadeArrNumber].volume = bgm_Manager.bgmDataList[RequesBgmFadeArrNumber / 2].bgmVolume;
            bgm[PlayBGMArrNumber].volume = 0.0f;
            return false;
        }
        else
        {
            bgm[PlayBGMArrNumber].volume -= faeOutVolume;
            bgm[RequesBgmFadeArrNumber].volume += fadeInVolume;
            return true;
        }
    }
}
