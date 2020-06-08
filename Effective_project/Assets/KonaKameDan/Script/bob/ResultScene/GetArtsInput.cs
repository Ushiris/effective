using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetArtsInput : MonoBehaviour
{
    // 入れ物
    public TextMeshProUGUI[] artsName;  // アーツ名

    // 入力するもの
    private string[] getArtsName;       //　アーツ名
    void Start()
    {
        for (int i = 0; i < MyArtsDeck.GetArtsDeck.Count; i++)// 最終的に所持していたアーツ名
        {
            getArtsName[i] = MyArtsDeck.GetArtsDeck[i].name;
        }
        ArtsNameInputDisplay();
    }

    /// <summary>
    /// アーツ名の書き込み
    /// </summary>
    private void ArtsNameInputDisplay()
    {
        for (int i = 0; i < MyArtsDeck.GetArtsDeck.Count; i++)// 最終的に所持していたアーツ名
            artsName[i].text = getArtsName[i];
    }
}
