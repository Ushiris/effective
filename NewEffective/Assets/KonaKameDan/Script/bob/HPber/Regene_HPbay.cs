using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Regene_HPbay : MonoBehaviour
{
    Image image;
    private void Start()
    {
        image = GetComponent<Image>();
    }
    void Update()
    {
        if (PlayerManager.GetManager.IsRegene())
        {
            image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        else
        {
            image.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
    }
}
