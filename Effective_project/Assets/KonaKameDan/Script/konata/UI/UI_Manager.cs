using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [Header("アーツを作るUI表示")]
    [SerializeField] KeyCode effectFusionUI_ActiveKey = KeyCode.E;
    public GameObject effectFusionUI_Obj;
    public GameObject effectFusionUI_CircleMakeByPizza;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        EffectFusionUI();
    }

    void EffectFusionUI()
    {
        //アーツを作るUI表示
        if (EffectFusionUI_ActiveTrigger())
        {
            if (!effectFusionUI_Obj.activeSelf)
            {
                effectFusionUI_Obj.SetActive(true);
                effectFusionUI_CircleMakeByPizza.SetActive(true);
            }
            else
            {
                effectFusionUI_Obj.SetActive(false);
                effectFusionUI_CircleMakeByPizza.SetActive(false);
            }
        }
    }

    /// <summary>
    /// アーツを作るUI表示キー
    /// </summary>
    /// <returns></returns>
    bool EffectFusionUI_ActiveTrigger()
    {
        return Input.GetKeyDown(effectFusionUI_ActiveKey);
    }

    /// <summary>
    /// 選択したエフェクトのリスト
    /// </summary>
    /// <returns></returns>
    public static List<int> GetEffectFusionUI_ChoiceList()
    {
        return new List<int>(EffectFusionUi.GetEffectFusionList);
    }
}
