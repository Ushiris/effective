using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーがアーツを出す処理
/// </summary>
public class PlayerArtsInstant : MonoBehaviour
{
    [SerializeField] string debugNum = "045";
    MyEffectCount myEffectCount;

    // Start is called before the first frame update
    void Start()
    {
        myEffectCount = GetComponent<MyEffectCount>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OnTrigger())
        {
            //アーツが出る場所
            Transform artsPivot = PlayerManager.GetManager.GetPlObj.transform.GetChild(3);

            GetEffectCount();

            //アーツを出す処理
            string artsId = ArtsInstantManager.SelectArts(MyArtsDeck.GetSelectArtsDeck.id, debugNum);
            ArtsInstantManager.InstantArts(artsPivot, artsId);
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
