using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_InformationCollection : MonoBehaviour
{
    private BGM_Manager bgm_Manager;
    /// <summary>
    /// BGMの名前
    /// </summary>
    public enum BGM_NAME
    {
        forest_BGM,     // ステージ1＿森
        forest_Boss_BGM // ステージ1＿森＿ボス曲
    }
    /// <summary>
    /// BGMの冒頭かループか
    /// </summary>
    public enum BGM_TYPE
    {
        beginning, loop
    }
    [System.Serializable]
    public class bgmInformation
    {
        [Header("要素の名前")]
        public string elementName;
        [Header("音源")]
        public AudioClip bgmSource;
        [Header("BGMの名前")]
        public BGM_NAME bgmName;
        [Header("BGMが冒頭かループか")]
        public BGM_TYPE bgmType;
        [Header("Boss曲か否か")]
        public bool whetherBossBgm;
        [Header("ボリューム調整")]
        [Range(0, 1)]
        public float bgmVolume;
        [Header("現在のボリューム")]
        [Range(0, 1)]
        [HideInInspector] public float bgmVolumeNow;
        [Header("基礎のボリューム")]
        [Range(0, 1)]
        public float bgmVolumeBasics;
        [Header("現在流れているか否か")]
        public bool bgmPlaybackNow;
        [Header("現在フェードインしてるか否か")]
        public bool bgmFadeInNow;
        [Header("現在フェードアウトしてるか否か")]
        public bool bgmFadeOutNow;
    }
    /// <summary>
    /// BGMの情報
    /// </summary>
    [Header("BGMの情報集")]
    public List<bgmInformation> bgmDataList = new List<bgmInformation>();
    /// <summary>
    /// ゲーム開始時、再生するBGMの名前
    /// </summary>
    [Header("ゲーム開始時、再生するBGMの名前")]
    public BGM_NAME firstPlay_bgm;

    private void Start()
    {
        bgm_Manager = GetComponent<BGM_Manager>();
        // ゲーム開始時、BGMを再生する
        BGM_Manager.BgmPlayback(firstPlay_bgm, BGM_TYPE.beginning, true);
    }
    private void Update()
    {
        for (int i = 0; i < bgmDataList.Count; i++)
        {
            // ボリューム変更したとき
            if (bgmDataList[i].bgmVolumeNow != bgmDataList[i].bgmVolume)
                bgm_Manager.VolumeChange(bgmDataList[i].bgmName, bgmDataList[i].bgmVolume, true);
        }
    }

    /// <summary>
    /// ボリュームの変更を情報集に記録する
    /// </summary>
    /// <param name="bgmName">ボリュームを変更したBGMの名前</param>
    /// <param name="volumeChange">変更したボリューム量</param>
    public void Volume_InformationChange(BGM_NAME bgmName, float volumeChange, bool bgmVolumeBasicsMemory) 
    {
        // 冒頭
        bgmDataList[(int)bgmName * 2].bgmVolume = volumeChange;
        bgmDataList[(int)bgmName * 2].bgmVolumeNow = volumeChange;
        // ループ
        bgmDataList[(int)bgmName * 2 + 1].bgmVolume = volumeChange;
        bgmDataList[(int)bgmName * 2 + 1].bgmVolumeNow = volumeChange;
        if(bgmVolumeBasicsMemory)
        {
            bgmDataList[(int)bgmName * 2].bgmVolumeBasics = volumeChange;
            bgmDataList[(int)bgmName * 2 + 1].bgmVolumeBasics = volumeChange;
        }
    }
    /// <summary>
    /// BGMが再生したことを情報集に記録する
    /// </summary>
    /// <param name="bgmName">再生したBGMの名前</param>
    /// <param name="bgmType">再生したBGMが冒頭かループか</param>
    /// <param name="reset">一度すべてのBGMを初期化し、初めてBGMを再生するときに true、そうでなければ false</param>
    public void Playback_InfomationChange(BGM_NAME bgmName, BGM_TYPE bgmType, bool reset)
    {
        if(reset)
        {
            for (int i = 0; i < bgmDataList.Count; i++)// 初期化
            {
                bgmDataList[i].bgmPlaybackNow = false;// 一度すべてリセット
            }
        }
        bgmDataList[(int)bgmName * 2 + (int)bgmType].bgmPlaybackNow = true;   // 再生したことを記録
    }
    /// <summary>
    /// BGMの再生が終わったことを情報集に記録する
    /// </summary>
    /// <param name="bgmName">再生が終わったBGMの名前</param>
    /// <param name="bgmType">再生が終わったBGMが冒頭かループか</param>
    public void NotPlayback_InfomationChange(BGM_NAME bgmName, BGM_TYPE bgmType)
    {
        bgmDataList[(int)bgmName * 2 + (int)bgmType].bgmPlaybackNow = false;   // 再生が終わったことを記録
    }
    /// <summary>
    /// フェードイン処理することを情報集に記録する
    /// </summary>
    /// <param name="bgmName">フェードイン処理するBGMの名前</param>
    /// <param name="bgmType">フェードイン処理するBGMが冒頭かループか</param>
    public void FadeIn_InformationChange(BGM_NAME bgmName, BGM_TYPE bgmType)
    {
        bgmDataList[(int)bgmName * 2 + (int)bgmType].bgmFadeInNow = true;   // フェードイン　:☑
        bgmDataList[(int)bgmName * 2 + (int)bgmType].bgmFadeOutNow = false; // フェードアウト:□
    }
    /// <summary>
    /// フェードアウト処理することを情報集に記録する
    /// </summary>
    /// <param name="bgmName">フェードアウト処理するBGMの名前</param>
    /// <param name="bgmType">フェードアウト処理するBGMが冒頭かループか</param>
    public void FadeOut_InformationChange(BGM_NAME bgmName, BGM_TYPE bgmType)
    {
        bgmDataList[(int)bgmName * 2 + (int)bgmType].bgmFadeInNow = false;  // フェードイン　:□
        bgmDataList[(int)bgmName * 2 + (int)bgmType].bgmFadeOutNow = true;  // フェードアウト:☑
    }
}
