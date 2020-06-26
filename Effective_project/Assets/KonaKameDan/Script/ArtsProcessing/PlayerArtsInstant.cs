using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーがアーツを出す処理
/// </summary>
public class PlayerArtsInstant : MonoBehaviour
{
    [SerializeField] string debugNum = "045";
    [SerializeField] GameObject artsObj;
    MyEffectCount myEffectCount;

    // Start is called before the first frame update
    void Start()
    {
        myEffectCount = artsObj.GetComponent<MyEffectCount>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OnTrigger())
        {
            GetEffectCount();

            //アーツを出す処理
            string artsId = ArtsInstantManager.SelectArts(MyArtsDeck.GetSelectArtsDeck.id, debugNum);
            ArtsInstantManager.InstantArts(artsObj.transform, artsId);
        }
    }

    //所持しているエフェクトのスタック数を入れる
    void GetEffectCount()
    {
        foreach(var item in MainGameManager.GetPlEffectList)
        {
            myEffectCount.effectCount[(NameDefinition.EffectName)item.id] = item.count;
        }
    }

    //アーツを放つキー
    bool OnTrigger()
    {
        return Input.GetMouseButtonDown(0) && !UI_Manager.GetIsEffectFusionUI_ChoiceActive;
    }
}
