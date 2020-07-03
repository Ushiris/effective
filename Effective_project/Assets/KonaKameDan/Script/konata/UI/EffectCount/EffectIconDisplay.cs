using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// エフェクトアイコンの表示
/// </summary>
public class EffectIconDisplay : MonoBehaviour
{
    public int num;
    Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        image.sprite = UI_Image.GetUI_Image.effectIcon[num].effectIcon;
    }
}
