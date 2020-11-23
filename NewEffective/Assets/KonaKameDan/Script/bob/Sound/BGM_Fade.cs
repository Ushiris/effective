using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Fade : MonoBehaviour
{
    private BGM_Manager bgm_Manager;
    private BGM_InformationCollection bgm_InformationCollection;

    [Header("フェードインのスピード")]
    [Range(0, 1)]
    [SerializeField] float fadeInSpead = 0.005f;
    [Header("フェードアウトのスピード")]
    [Range(0, 1)]
    [SerializeField] float fadeOutSpead = 0.005f;

    /// <summary>
    /// フェード処理をしているとき true、していないとき false
    /// </summary>
    public static bool fadeModeOn { get; set; }
    private void Start()
    {
        bgm_Manager = GetComponent<BGM_Manager>();
        bgm_InformationCollection = GetComponent<BGM_InformationCollection>();
    }
    public void Update()
    {
        for (int i = 0; i < bgm_InformationCollection.bgmDataList.Count; i++)
        {
            if(bgm_InformationCollection.bgmDataList[i].bgmFadeInNow)
            {
                BGM_Manager.BgmFadeInModeOn(bgm_InformationCollection.bgmDataList[i].bgmName, bgm_InformationCollection.bgmDataList[i].bgmType, fadeInSpead);
            }

            if (bgm_InformationCollection.bgmDataList[i].bgmFadeOutNow)
            {
                BGM_Manager.BgmFadeOutModeOn(bgm_InformationCollection.bgmDataList[i].bgmName, bgm_InformationCollection.bgmDataList[i].bgmType, fadeOutSpead);
            }
        }
    }
}
