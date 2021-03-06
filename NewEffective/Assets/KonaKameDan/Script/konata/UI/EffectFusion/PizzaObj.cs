﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ピザの角度を動的に変更する
/// </summary>
public class PizzaObj : MonoBehaviour
{
    [SerializeField] GameObject outPizzaImage;
    [SerializeField] GameObject inPizzaImage;
    public int cutNum;
    public bool isChangeColor;

    public Color color;

    void OnEnable()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        cutNum = UI_Manager.GetUI_Manager.EffectListCount;

        //微調整用
        GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, (360 / cutNum) / 2);

        //外枠と内側の角度の調整
        outPizzaImage.GetComponent<Image>().fillAmount = 1 / (float)cutNum;
        inPizzaImage.GetComponent<Image>().fillAmount = (1 / (float)cutNum) - 0.01f;       
    }

    // Update is called once per frame
    void Update()
    {
        //色を変える
        if (isChangeColor)
        {
            color = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 0.5f);  
        }
        else
        {
            color = new Color(Color.black.r, Color.black.g, Color.black.b, 0.5f);
        }
        outPizzaImage.GetComponent<Image>().color = color;
    }
}
