using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArtsExplanatoryText : MonoBehaviour
{
    [SerializeField] private ArtsTextDictionary artsTextDictionary;
    [SerializeField] private string artsName;
    private string artsNameTmp;
    public TextMeshProUGUI artsText;
    private void Start()
    {
        artsTextDictionary = GetComponent<ArtsTextDictionary>();
    }
    void Update()
    {
        artsName = MyArtsDeck.GetSelectArtsDeck.id;
        if (artsName == null)// 何もないアーツを選択
        {
            artsText.text = ("");// 説明文入力
        }
        else if (artsName != artsNameTmp)// 現在選択しているアーツの説明文
        {
            artsText.text = artsTextDictionary.ArtsText[artsName];// 説明文入力
        }
        artsNameTmp = artsName;
    }
}
