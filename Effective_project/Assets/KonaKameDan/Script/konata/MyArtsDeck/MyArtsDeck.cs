using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 作成したアーツを登録する
/// </summary>
public class MyArtsDeck : MonoBehaviour
{
    static List<ArtsList.ArtsData> GetMyArtsDeck = new List<ArtsList.ArtsData>();

    bool selectArtsReset;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GetMyArtsDeck.Add(new ArtsList.ArtsData());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (OnTrigger())
        {
            if (UI_Manager.GetEffectFusionUI_ChoiceNum.numList.Count > 1)
            {
                int num = UI_Manager.GetChoiceArtsDeckNum;
                GetMyArtsDeck[num] = ArtsList.GetSelectArts;
            }
        }

    }
    bool OnTrigger()
    {
        return UI_Manager.ArtsEntryTrigger() && UI_Manager.GetIsEffectFusionUI_ChoiceActive;
    }

    /// <summary>
    /// 選択しているアーツ情報を取得
    /// </summary>
    public static ArtsList.ArtsData GetSelectArtsDeck
    {
        get
        {
            return GetMyArtsDeck[UI_Manager.GetChoiceArtsDeckNum];
        }
    }

    /// <summary>
    /// 所持しているアーツを全て取得
    /// </summary>
    public static List<ArtsList.ArtsData> GetArtsDeck
    {
        get
        {
            return new List<ArtsList.ArtsData>(GetMyArtsDeck);
        }
    }
}
