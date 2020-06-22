using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectObjectInput : MonoBehaviour
{
    public Image[] effectObjectImage;   // エフェクトオブジェクトの入れ物
    private Sprite[] effectObject = new Sprite[9];      // エフェクトオブジェクトの画像
    private int effectObjectMax = 3;    // エフェクトオブジェクト最大数
    void Start()
    {
        for (int n = 0; n < effectObjectImage.Length; n++)
            effectObjectImage[n].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);// Imageの透明化

        for (int i = 0; i < MyArtsDeck.GetArtsDeck.Count; i++)// 最終的に所持していたアーツ数
        {
            for(int y = 0; y < MyArtsDeck.GetArtsDeck[i].effectList.Count; y++)// 最終的に所持していたエフェクトオブジェクト数
            {
                effectObjectImage[y + i * effectObjectMax].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);// Imageの可視化
                effectObject[y + i * effectObjectMax] = UI_Image.GetUI_Image.effectIcon[MyArtsDeck.GetArtsDeck[i].effectList[y]].effectIcon;// エフェクトオブジェクトの画像検索
                effectObjectImage[y + i * effectObjectMax].sprite = effectObject[y + i * effectObjectMax];// エフェクトオブジェクトの画像挿入
            }
        }
    }
}
