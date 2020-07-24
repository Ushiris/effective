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

    Dictionary<string, float> coolTimes = new Dictionary<string, float>();
    List<string> removeKey = new List<string>();

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

            //ArtsID検出
            string artsId = ArtsInstantManager.SelectArts(MyArtsDeck.GetSelectArtsDeck.id, debugNum);

            ArtsInstantManager.InstantArts(artsObj, artsId);
        }
    }

    //クールタイム用
    void CoolTime(string artsId)
    {
        //ここにそれぞれのクールタイムを入れる
        float timer = ArtsCoolTime.GetCoolTime(artsId, myEffectCount);

        //生成
        if (!coolTimes.ContainsKey(artsId))
        {
            ArtsInstantManager.InstantArts(artsObj, artsId);
            coolTimes.Add(artsId, timer);
        }

        //タイマーの処理
        foreach(var key in coolTimes.Keys)
        {
            coolTimes[key] -= Time.deltaTime;
            if (coolTimes[key] < 0) removeKey.Add(key);
        }

        //タイマーを消す
        if (removeKey.Count != 0)
        {
            foreach (var key in removeKey)
            {
                coolTimes.Remove(key);
            }
            removeKey.Clear();
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
