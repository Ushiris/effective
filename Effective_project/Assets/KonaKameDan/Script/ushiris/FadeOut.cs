using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FadeOut : MonoBehaviour
{
    Image Fade;

    private void Start()
    {
        Fade = GetComponent<Image>();
    }
}
