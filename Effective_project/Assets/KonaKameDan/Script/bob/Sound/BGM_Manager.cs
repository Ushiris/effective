using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Manager : MonoBehaviour
{
    [SerializeField] int audioInstantMaxCount = 3;// そのシーンで流すBGMの最大の数

    AudioSource[] bgmArr;

    /// <summary>
    /// BGMの種類名
    /// </summary>
    public enum BGM_NAME
    {
        Stage_1
    }

    [System.Serializable]
    /// <summary>
    /// BGMのパート分け
    /// </summary>
    public class bgmData
    {
        public BGM_NAME name;
        public AudioClip startBGM;
        public AudioClip loopBGM;
        //public AudioClip bossBGM;// 仕様がどうなるのかわからないので保留
    }
    public List<bgmData> bgmDataList = new List<bgmData>();

    static BGM_Manager BGM_Manager_;
    private bool playBgmSwitch;

    void Start()
    {
        //オーディオをアタッチする
        for (int i = 0; i < audioInstantMaxCount; i++)
        {
            gameObject.AddComponent<AudioSource>();
        }
        bgmArr = GetComponents<AudioSource>();

        for (int i = 0; i < audioInstantMaxCount; i++)
        {
            bgmArr[i].volume = 0.5f;
            if(i != 0)// 前奏以外をループに
                bgmArr[i].loop = true;
        }

        BGM_Manager_ = this;
    }
    private void Update()
    {
        if(playBgmSwitch)
        {
            bgmArr[1].Stop();// 停止
            //bgmArr[2].Stop();// 停止
            bgmArr[0].Play();// 再生
            playBgmSwitch = false;
        }
        if(!bgmArr[0].isPlaying && !bgmArr[1].isPlaying)
        {
            bgmArr[1].Play();// 再生
            //bgmArr[2].Play();// 再生
        }
    }
    /// <summary>
    /// BGMの再生、どのBGMを使うか選択して！
    /// </summary>
    /// <param name="bgmType"></param>
    public static void BgmPlay(BGM_NAME bgmType)
    {
        //BGMのパートを各自入れる
        if (BGM_Manager_ != null)
        {
            BGM_Manager_.bgmArr[0].clip = BGM_Manager_.bgmDataList[(int)bgmType].startBGM;// 前奏
            BGM_Manager_.bgmArr[1].clip = BGM_Manager_.bgmDataList[(int)bgmType].loopBGM;// ループ

            BGM_Manager_.playBgmSwitch = true;
        }
    }
}