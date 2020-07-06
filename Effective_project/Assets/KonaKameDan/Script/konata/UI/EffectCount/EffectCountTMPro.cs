using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// 所持しているエフェクトの数表示
/// </summary>
public class EffectCountTMPro : MonoBehaviour
{
    public int num;
    TextMeshProUGUI textMeshPro;

    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textMeshPro.text = "x" + MainGameManager.GetPlEffectList[num].count;
    }
}
