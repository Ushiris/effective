using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// カーソルのある位置のアイテム名を出す(ピザ)
/// </summary>
public class MouseChaseIcon : MonoBehaviour
{
    Image img;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        int num = UI_Manager.GetEffectFusionUI_ChoiceNum.num;
        img.sprite = UI_Image.GetUI_Image.effectIcon[num].effectIcon;
    }
}
