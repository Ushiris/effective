using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 作成したアーツを登録する
/// </summary>
public class MyArtsDeck : MonoBehaviour
{
    public delegate void Action();
    public static Action SetArtsSelectAfterProcess;

    static List<ArtsList.ArtsData> GetMyArtsDeck;

    // Start is called before the first frame update
    void Awake()
    {
        //初期化
        if (MainGameManager.GetArtsReset || GetMyArtsDeck == null)
        {
            GetMyArtsDeck = new List<ArtsList.ArtsData>();

            //初期化
            for (int i = 0; i < transform.childCount; i++)
            {
                GetMyArtsDeck.Add(new ArtsList.ArtsData());
            }
        }

        SetArtsSelectAfterProcess = () => { };
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

                //エフェクト所持数チェック
                for (int i = 0; i < GetMyArtsDeck[num].effectStockCount.Length; i++)
                {
                    try
                    {
                        GetMyArtsDeck[num].effectStockCount[i] =
                            SearchEffectStockCount(GetMyArtsDeck[num].effectList[i]);
                    }
                    catch (System.ArgumentException)
                    {
                        GetMyArtsDeck[num].effectStockCount[i] = 0;
                    }

                }

                SetArtsSelectAfterProcess();
            }
        }
    }

    bool OnTrigger()
    {
        return UI_Manager.ArtsEntryTrigger() && UI_Manager.GetIsEffectFusionUI_ChoiceActive;
    }

    //所持しているエフェクトからアーツに組み込まれたエフェクトごとの所持数を返す
    int SearchEffectStockCount(int num)
    {
        int count = 0;
        foreach(var item in MainGameManager.GetPlEffectList)
        {
            if (item.id == num)
            {
                count = item.count;
                break;
            }
        }
        return count;
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
