using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FusionCircleColorControl : MonoBehaviour
{
    public enum ColorMode { Before, After, Custom }

    [SerializeField] Color beforeColor;
    [SerializeField] Color afterColor;

    public ColorMode GetColorMode { get; private set; }

    Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        ColorChangeBefore();
    }

    /// <summary>
    /// 選択されていない時の色
    /// </summary>
    public void ColorChangeBefore()
    {
        image.color = beforeColor;
        GetColorMode = ColorMode.Before;
    }

    /// <summary>
    /// 選択されたときの色
    /// </summary>
    public void ColorChangeAfter()
    {
        image.color = afterColor;
        GetColorMode = ColorMode.After;
    }

    /// <summary>
    /// 色を変える
    /// </summary>
    /// <param name="color"></param>
    public void ColorChange(Color32 color)
    {
        image.color = color;
    }
}
