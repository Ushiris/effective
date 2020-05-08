﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// カーソルのある位置のアイテム名を出す(ピザ)
/// </summary>
public class UIText : MonoBehaviour
{
    Text txt;
    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = UI_Manager.GetEffectFusionUI_ChoiceNum.num.ToString();
    }
}
