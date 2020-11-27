using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenuSelectIcon : MonoBehaviour
{
    Vector3 siz;

    private void Start()
    {
        siz = transform.localScale;
    }

    public bool OnSizChange()
    {
        transform.localScale = siz * 1.3f;
        return true;
    }

    public bool OnDefaultSiz()
    {
        transform.localScale = siz;
        return false;
    }
}
