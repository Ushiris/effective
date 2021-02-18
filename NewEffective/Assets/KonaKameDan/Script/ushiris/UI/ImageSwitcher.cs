using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSwitcher : MonoBehaviour
{
    [SerializeField] Image on, off;
    [SerializeField] bool isHide = true;

    private void Awake()
    {
        if (on == null) on = transform.Find("on").gameObject.GetComponent<Image>();
        if (off == null) off = transform.Find("off").gameObject.GetComponent<Image>();

        Hide();
    }

    public void Switch(bool IsOn)
    {
        if (isHide) return;

        Visible(on, IsOn);
        Visible(off, !IsOn);
    }

    void Visible(Image img, bool state)
    {
        var color = img.color;
        color.a = state ? 1 : 0;
        img.color = color;
    }

    public void Hide()
    {
        Visible(on, false);
        Visible(off, false);
        isHide = true;
    }

    public void Show(bool state)
    {
        Switch(state);
        isHide = false;
    }
}
