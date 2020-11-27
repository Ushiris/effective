using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayBGM : MonoBehaviour
{
    [Header("キーボード[B]でBoss曲を再生")]
    [SerializeField] private bool boosBGM = false;
    [Header("キーボード[G]でBoss曲をフェードイン")]
    [SerializeField] private bool boosBGMFadeIn = false;
    [Header("キーボード[T]でBoss曲をフェードアウト")]
    [SerializeField] private bool boosBGMFadeOut = false;
    [Header("キーボード[N]でnatural(ステージ)曲を再生")]
    [SerializeField] private bool naturalBGM = false;
    [Header("キーボード[H]でnatural(ステージ)曲をフェードイン")]
    [SerializeField] private bool naturalBGMFadeIn = false;
    [Header("キーボード[Y]でnatural(ステージ)曲をフェードアウト")]
    [SerializeField] private bool naturalBGMFadeOut = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            boosBGM = true;
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            naturalBGM = true;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            boosBGMFadeIn = true;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            naturalBGMFadeIn = true;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            boosBGMFadeOut = true;
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            naturalBGMFadeOut = true;
        }

        if (boosBGM)
        {
            // BGM:( forest_Boss_BGM )の冒頭:( beginning )を再生したい時の書き方
            BGM_Manager.BgmPlayback(BGM_InformationCollection.BGM_NAME.forest_Boss_BGM, BGM_InformationCollection.BGM_TYPE.beginning, true);
            boosBGM = false;
        }

        if (naturalBGM)
        {
            // BGM:( forest_BGM )の冒頭:( beginning )を再生したい時の書き方
            BGM_Manager.BgmPlayback(BGM_InformationCollection.BGM_NAME.forest_BGM, BGM_InformationCollection.BGM_TYPE.beginning, true);
            naturalBGM = false;
        }

        if (boosBGMFadeIn)
        {
            // BGM:( forest_Boss_BGM )をフェードインしたい時の書き方
            BGM_Manager.BgmFadeIn(BGM_InformationCollection.BGM_NAME.forest_Boss_BGM);
            boosBGMFadeIn = false;
        }

        if (naturalBGMFadeIn)
        {
            // BGM:( forest_BGM )をフェードインしたい時の書き方
            BGM_Manager.BgmFadeIn(BGM_InformationCollection.BGM_NAME.forest_BGM);
            naturalBGMFadeIn = false;
        }

        if (boosBGMFadeOut)
        {
            // BGM:( forest_Boss_BGM )をフェードアウトしたい時の書き方
            BGM_Manager.BgmFadeOut(BGM_InformationCollection.BGM_NAME.forest_Boss_BGM);
            boosBGMFadeOut = false;
        }

        if (naturalBGMFadeOut)
        {
            // BGM:( forest_BGM )をフェードアウトしたい時の書き方
            BGM_Manager.BgmFadeOut(BGM_InformationCollection.BGM_NAME.forest_BGM);
            naturalBGMFadeOut = false;
        }
    }
}
