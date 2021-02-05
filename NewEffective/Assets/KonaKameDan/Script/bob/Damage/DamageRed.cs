using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageRed : MonoBehaviour
{
    private Image damageImg;
    public static bool damageHidRed { private get; set; }
    void Start()
    {
        damageImg = GetComponent<Image>();
        damageImg.color = Color.clear;
        damageHidRed = false;
    }

    void Update()
    {
        if(damageHidRed)
        {
            this.damageImg.color = new Color(0.5f, 0.0f, 0.0f, 0.5f);
            damageHidRed = false;
        }
        else
        {
            this.damageImg.color = Color.Lerp(this.damageImg.color, Color.clear, Time.deltaTime);
        }
    }

    public void DamageHitRed()
    {
        damageHidRed = true;
    }
}
