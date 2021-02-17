using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSwitcher : MonoBehaviour
{
    [SerializeField] Image on, off;

    private void Awake()
    {
        if (on == null) on = transform.Find("on").gameObject.GetComponent<Image>();
        if (off == null) off = transform.Find("off").gameObject.GetComponent<Image>();

        Switch(false);
    }

    public void Switch(bool IsOn)
    {
        Visible(on, IsOn);
        Visible(off, !IsOn);
    }

    void Visible(Image img, bool state)
    {
        var color = img.color;
        color.a = state ? 1 : 0;
        img.color = color;
    }
}
