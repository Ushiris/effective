using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FusionCircleColorControl : MonoBehaviour
{
    public enum ColorMode { Before, After, Lock, Custom }

    [SerializeField] Color beforeColor;
    [SerializeField] Color afterColor;
    [SerializeField] Color lockColor;
    [SerializeField] Image icon;

    public ColorMode GetColorMode { get; private set; }

    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    /// <summary>
    /// ロック時の色
    /// </summary>
    public void ColorChangeLock()
    {
        if (icon != null)
        {
            icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 0.1f);
        }
        image.color = lockColor;
        GetColorMode = ColorMode.Lock;
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
