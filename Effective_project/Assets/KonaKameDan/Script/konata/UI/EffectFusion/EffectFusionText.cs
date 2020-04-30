
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 所持しているエフェクト表示(デバック)
/// </summary>
public class EffectFusionText : MonoBehaviour
{
    [SerializeField] Text txt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = "";
        for (int i = 0; i < UI_Manager.GetEffectFusionUI_ChoiceList().Count; i++)
        {
            txt.text += " " + UI_Manager.GetEffectFusionUI_ChoiceList()[i];
        }
    }
}
