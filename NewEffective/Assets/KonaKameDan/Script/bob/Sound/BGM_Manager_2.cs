using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Manager_2 : MonoBehaviour
{
    private static AudioSource[] bgmAudioSourceArr;
    private BGM_InformationCollection_2 bgm_InformationCollection;
    private static BGM_Manager_2 bgm_Manager;
    void Awake()
    {
        bgm_Manager = this;
        bgm_InformationCollection = GetComponent<BGM_InformationCollection_2>();

        // AudioSourceをAudioClipの数だけAddComponentする
        for (int i = 0; i < bgm_InformationCollection.bgmDataList.Count; i++)
            gameObject.AddComponent<AudioSource>();
        // 現在アタッチされているAudioSourceを読み込む
        bgmAudioSourceArr = GetComponents<AudioSource>();

        /* AudioSourceにBGMの情報を入れる */
        for (int i = 0; i < bgm_InformationCollection.bgmDataList.Count; i++)
        {
            bgm_InformationCollection.bgmDataList[i].bgmVolumeNow = bgm_InformationCollection.bgmDataList[i].bgmVolume;// ボリューム量を保管
            bgmAudioSourceArr[i].clip = bgm_InformationCollection.bgmDataList[i].bgmSource;// 音源をアタッチ
            bgmAudioSourceArr[i].playOnAwake = false;// playOnAwakeを切っておく（初っ端流れないようにする）
            if (bgm_InformationCollection.bgmDataList[i].bgmType == BGM_InformationCollection_2.BGM_TYPE.beginning)// 冒頭かループか
            {
                bgmAudioSourceArr[i].loop = false;  // 冒頭
            }
            else if (bgm_InformationCollection.bgmDataList[i].bgmType == BGM_InformationCollection_2.BGM_TYPE.loop)
            {
                bgmAudioSourceArr[i].loop = true;   // ループ
            }
            bgmAudioSourceArr[i].volume = bgm_InformationCollection.bgmDataList[i].bgmVolume;// ボリューム量を調整
        }
        ////////////////////////////////////
    }
    private void Update()
    {
        for (int i = 0; i < bgm_InformationCollection.bgmDataList.Count; i++)
        {
            /* [冒頭 → ループ → ループ → ∞]このように音源が再生される処理をしているところ */
            if (bgm_InformationCollection.bgmDataList[i].bgmPlaybackNow                                             // 記録上、再生されていることになっているBGMで
            && bgm_InformationCollection.bgmDataList[i].bgmType == BGM_InformationCollection_2.BGM_TYPE.beginning   // それが冒頭のBGMであり、
            && !bgmAudioSourceArr[i].isPlaying)                                                                     // 現在、再生が終わった時
            {
                // 再生が終わったことを記録させる
                bgm_InformationCollection.NotPlayback_InfomationChange(bgm_InformationCollection.bgmDataList[i].bgmName, bgm_InformationCollection.bgmDataList[i].bgmType);
                // 冒頭の続き、ループを再生
                BgmPlayback(bgm_InformationCollection.bgmDataList[i].bgmName, BGM_InformationCollection_2.BGM_TYPE.loop, bgm_InformationCollection.bgmDataList[i].bgmVolume, false);
            }
            ////////////////////////////////////////////////////////////////////////////////////
        }
    }
    /// <summary>
    /// ボリューム変更
    /// </summary>
    /// <param name="bgmName">変更したいBGMの名前</param>
    /// <param name="bgmVolumeChange">変更したいボリューム量</param>
    public void VolumeChange(BGM_InformationCollection_2.BGM_NAME bgmName, float bgmVolumeChange)
    {
        bgmAudioSourceArr[(int)bgmName * 2].volume = bgmVolumeChange;       // 冒頭
        bgmAudioSourceArr[(int)bgmName * 2 + 1].volume = bgmVolumeChange;   // ループ
        bgm_InformationCollection.Volume_InformationChange(bgmName, bgmVolumeChange);// 変更を記録させる
    }
    /// <summary>
    /// BGMを再生
    /// </summary>
    /// <param name="bgmName">再生したいBGMの名前</param>
    /// <param name="bgmType">再生したいのは、冒頭から？ループから？</param>
    /// <param name="volume">再生するボリューム量</param>
    /// <param name="reset">一度すべてのBGMを初期化し、初めてBGMを流すときに true、そうでなければ false</param>
    public static void BgmPlayback(BGM_InformationCollection_2.BGM_NAME bgmName, BGM_InformationCollection_2.BGM_TYPE bgmType, float volume, bool reset)
    {
        if (reset)
        {
            for (int i = 0; i < bgmAudioSourceArr.Length; i++)// 初期化
            {
                bgmAudioSourceArr[i].Stop();
            }
        }
        bgm_Manager.VolumeChange(bgmName, volume);// ボリューム変更
        bgmAudioSourceArr[(int)bgmName * 2 + (int)bgmType].Play();
        bgm_Manager.bgm_InformationCollection.Playback_InfomationChange(bgmName, bgmType, reset);// 変更を記録させる
    }
    /// <summary>
    /// BGMのフェードインを予約する
    /// </summary>
    /// <param name="bgmName">フェードインさせたいBGMの名前</param>
    /// <param name="bgmType">フェードインさせたいBGMは、冒頭？ループ？</param>
    /// <param name="volume">フェードインさせるさいの最終到達ボリューム量</param>
    public static void BgmFadeIn(BGM_InformationCollection_2.BGM_NAME bgmName, BGM_InformationCollection_2.BGM_TYPE bgmType, float volume)
    {
        bgm_Manager.bgm_InformationCollection.bgmDataList[(int)bgmName * 2 + (int)bgmType].bgmVolumeHildValue = volume;// 最終到達ボリューム量を記録させる
        bgm_Manager.bgm_InformationCollection.FadeIn_InformationChange(bgmName, bgmType);// 変更を記録させる
    }
    /// <summary>
    /// BGMのフェードアウトを予約する
    /// </summary>
    /// <param name="bgmName">フェードアウトさせたいBGMの名前</param>
    /// <param name="bgmType">フェードアウトさせたいのは、冒頭？ループ？</param>
    /// <param name="volume">フェードアウトさせるさいの最終到達ボリューム量</param>
    public static void BgmFadeOut(BGM_InformationCollection_2.BGM_NAME bgmName, BGM_InformationCollection_2.BGM_TYPE bgmType, float volume)
    {
        bgm_Manager.bgm_InformationCollection.bgmDataList[(int)bgmName * 2 + (int)bgmType].bgmVolumeHildValue = volume;// 最終到達ボリューム量を記録させる
        bgm_Manager.bgm_InformationCollection.FadeOut_InformationChange(bgmName, bgmType);// 変更を記録させる
    }
    /// <summary>
    /// フェードイン処理を実行
    /// </summary>
    /// <param name="bgmName">フェードイン処理させてるBGMの名前</param>
    /// <param name="bgmType">フェードイン処理させてるBGMは、冒頭？ループ？</param>
    /// <param name="fadeIn">フェードインするスピード</param>
    public static void BgmFadeInModeOn(BGM_InformationCollection_2.BGM_NAME bgmName, BGM_InformationCollection_2.BGM_TYPE bgmType, float fadeIn)
    {
        if (!bgmAudioSourceArr[(int)bgmName * 2 + (int)bgmType].isPlaying)
            bgmAudioSourceArr[(int)bgmName * 2 + (int)bgmType].Play();

        bgm_Manager.VolumeChange(bgmName, (bgmAudioSourceArr[(int)bgmName * 2 + (int)bgmType].volume) + fadeIn);// ボリューム変更
        if (bgmAudioSourceArr[(int)bgmName * 2 + (int)bgmType].volume > bgm_Manager.bgm_InformationCollection.bgmDataList[(int)bgmName * 2 + (int)bgmType].bgmVolumeHildValue)
        {
            bgm_Manager.VolumeChange(bgmName, bgm_Manager.bgm_InformationCollection.bgmDataList[(int)bgmName * 2 + (int)bgmType].bgmVolumeHildValue);// ボリューム変更_微調整
            bgmAudioSourceArr[(int)bgmName * 2 + (int)bgmType].volume = bgm_Manager.bgm_InformationCollection.bgmDataList[(int)bgmName * 2 + (int)bgmType].bgmVolumeHildValue;// 微調整
            bgm_Manager.bgm_InformationCollection.bgmDataList[(int)bgmName * 2 + (int)bgmType].bgmFadeInNow = false;// フェードイン処理を事項するためのチェックを外す
        }
    }
    /// <summary>
    /// フェードアウト処理を実行
    /// </summary>
    /// <param name="bgmName">フェードアウト処理させてるBGMの名前</param>
    /// <param name="bgmType">フェードアウト処理させてるBGMは、冒頭？ループ？</param>
    /// <param name="fadeIn">フェードアウトするスピード</param>
    public static void BgmFadeOutModeOn(BGM_InformationCollection_2.BGM_NAME bgmName, BGM_InformationCollection_2.BGM_TYPE bgmType, float fadeOut)
    {
        bgm_Manager.VolumeChange(bgmName, (bgmAudioSourceArr[(int)bgmName * 2 + (int)bgmType].volume) - fadeOut);// ボリューム変更
        if (bgmAudioSourceArr[(int)bgmName * 2 + (int)bgmType].volume <= bgm_Manager.bgm_InformationCollection.bgmDataList[(int)bgmName * 2 + (int)bgmType].bgmVolumeHildValue)
        {
            bgm_Manager.VolumeChange(bgmName, bgm_Manager.bgm_InformationCollection.bgmDataList[(int)bgmName * 2 + (int)bgmType].bgmVolumeHildValue);// ボリューム変更_微調整
            bgmAudioSourceArr[(int)bgmName * 2 + (int)bgmType].volume = bgm_Manager.bgm_InformationCollection.bgmDataList[(int)bgmName * 2 + (int)bgmType].bgmVolumeHildValue;// 微調整
            bgm_Manager.bgm_InformationCollection.bgmDataList[(int)bgmName * 2 + (int)bgmType].bgmFadeOutNow = false;// フェードアウト処理を事項するためのチェックを外す
        }
    }
}
