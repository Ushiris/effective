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
    /// BGMを再生
    /// </summary>
    /// <param name="bgmName">再生したいBGM</param>
    /// <param name="startVolume">BGMの音量</param>
    /// <param name="startOrLoop">「true」で冒頭から、「false」でループから</param>
    public static void BgmStartPlayBack(BGM_Manager.BGM_NAME bgmName,float startVolume, bool startOrLoop)
    {
        bgm_Name = bgmName;
        volume = startVolume;
        if(startOrLoop == true)
            playBgmStartSwitch = true;// 冒頭からスイッチ
        else if (startOrLoop == false)
            playBgmLoopSwitch = true;// ループからスイッチ
    }
    /// <summary>
    /// 現在流れているBGMのナンバー
    /// </summary>
    public static int PlayBGMArrNumber { get; private set; }
    /// <summary>
    /// フェードさせたいBGMのナンバーを入力
    /// </summary>
    public static BGM_Manager.BGM_NAME RequesBgmFadeArrNumber { get; set; }
    /// <summary>
    /// フェードイン、フェードアウトの値を受け取りAudioSourceに入力
    /// </summary>
    /// <param name="fadeInVolume">フェードインの値</param>
    /// <param name="faeOutVolume">フェードアウトの値</param>
    /// <returns></returns>
    public static bool OnFadeVolume (float fadeInVolume, float faeOutVolume)
    {
        if (bgm[(int)RequesBgmFadeArrNumber * 2].volume > bgm_Manager.bgmDataList[(int)RequesBgmFadeArrNumber].bgmVolume)
        {
            bgm[(int)RequesBgmFadeArrNumber * 2].volume = bgm_Manager.bgmDataList[(int)RequesBgmFadeArrNumber].bgmVolume;// フェードイン完了調整
            bgm[PlayBGMArrNumber].volume = 0.0f;// フェードアウト完了調整
            bgm_Name = bgm_Manager.bgmDataList[(int)RequesBgmFadeArrNumber].name;
            return false;
        }
        else
        {
            bgm[PlayBGMArrNumber].volume -= faeOutVolume;
            bgm[(int)RequesBgmFadeArrNumber * 2].volume += fadeInVolume;
            return true;
        }
    }
    /// <summary>
    /// ボリューム調整
    /// </summary>
    /// <param name="bgmVolumeChange">変えたいボリュームの大きさ</param>
    public static void VolumeChange(BGM_Manager.BGM_NAME　bgmNumber, float bgmVolumeChange)
    {
        bgm[(int)bgmNumber * 2].volume = bgmVolumeChange;
        bgm[(int)bgmNumber * 2 + 1].volume = bgmVolumeChange;
    }
}
