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
            //BGM
            BGM_Manager_2.BgmPlayback(BGM_InformationCollection_2.BGM_NAME.forest_Boss_BGM, BGM_InformationCollection_2.BGM_TYPE.beginning, 0.2f, true);
            boosBGM = false;
        }

        if (naturalBGM)
        {
            //BGM
            BGM_Manager_2.BgmPlayback(BGM_InformationCollection_2.BGM_NAME.forest_BGM, BGM_InformationCollection_2.BGM_TYPE.beginning, 0.2f, true);
            naturalBGM = false;
        }

        if (boosBGMFadeIn)
        {
            //BGM
            BGM_Manager_2.BgmFadeIn(BGM_InformationCollection_2.BGM_NAME.forest_Boss_BGM, BGM_InformationCollection_2.BGM_TYPE.beginning, 0.2f);
            boosBGMFadeIn = false;
        }

        if (naturalBGMFadeIn)
        {
            //BGM
            BGM_Manager_2.BgmFadeIn(BGM_InformationCollection_2.BGM_NAME.forest_BGM, BGM_InformationCollection_2.BGM_TYPE.beginning, 0.2f);
            naturalBGMFadeIn = false;
        }

        if (boosBGMFadeOut)
        {
            //BGM
            BGM_Manager_2.BgmFadeOut(BGM_InformationCollection_2.BGM_NAME.forest_Boss_BGM, BGM_InformationCollection_2.BGM_TYPE.beginning, 0.0f);
            boosBGMFadeOut = false;
        }

        if (naturalBGMFadeOut)
        {
            //BGM
            BGM_Manager_2.BgmFadeOut(BGM_InformationCollection_2.BGM_NAME.forest_BGM, BGM_InformationCollection_2.BGM_TYPE.beginning, 0.0f);
            naturalBGMFadeOut = false;
        }
    }
}
