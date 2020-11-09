using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Manager : MonoBehaviour
{
    AudioSource[] bgmArr;

    /// <summary>
    /// BGMの種類名
    /// </summary>
    public enum BGM_NAME
    {
        Stage_1, Stage_1_Boss
    }
    [Header("流れるBGM")]
    [SerializeField] BGM_NAME bgm_Name;
    private BGM_NAME bgm_NameOld;

    [System.Serializable]
    public class bgmData
    {
        [Header("BGMの名前")]
        public BGM_NAME name;
        [Header("冒頭音源")]
        public AudioClip startBGM;
        [Header("ループ音源")]
        public AudioClip loopBGM;
        [Header("ボリューム調整")]
        [Range(0, 1)]
        public float bgmVolume = 0.8f;
        public float bgmVolumeOld;
    }
    /// <summary>
    /// BGMの情報
    /// </summary>
    public List<bgmData> bgmDataList = new List<bgmData>();

    public static BGM_Manager BGM_Manager_;
    private bool playBgmSwitch;

    void Awake()
    {
        BGM_Manager_ = this;

        // オーディオをアタッチし
        for (int i = 0; i < bgmDataList.Count * 2; i++)
        {
            gameObject.AddComponent<AudioSource>();
        }
        bgmArr = GetComponents<AudioSource>();

        // AudioSourceをBGMごとにまとめる
        for (int i = 0; i < bgmDataList.Count; i++)
        {
            bgmDataList[i].bgmVolumeOld = bgmDataList[i].bgmVolume;

            bgmArr[i * 2].clip = bgmDataList[i].startBGM;// 冒頭の曲をアタッチ
            bgmArr[i * 2].playOnAwake = false;// 初っ端流れない
            bgmArr[i * 2 + 1].clip = bgmDataList[i].loopBGM;// ループの曲をアタッチ
            bgmArr[i * 2 + 1].playOnAwake = false;// 初っ端流れない
            bgmArr[i * 2 + 1].loop = true;// ループの曲をループ設定をしておく
        }

        Debug.Log("bgmDataList[(int)bgm_Name].bgmVolume : " + bgmDataList[(int)bgm_Name].bgmVolume);
        BGM_PlayBack.BgmStartPlayBack(bgm_Name, bgmDataList[(int)bgm_Name].bgmVolume, true);// 再生開始
        bgm_NameOld = bgm_Name;
    }
    private void Update()
    {
        if(bgm_NameOld != bgm_Name)
        {
            //BGM_Change.BgmChange(audioSourceDataList[(int)bgm_Name]);
            bgm_NameOld = bgm_Name;
        }

    }
}