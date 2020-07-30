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

    public static Dictionary<string, float> coolTimes = new Dictionary<string, float>();
    List<string> collectionKey = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        coolTimes.Clear();
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

            //ArtsInstantManager.InstantArts(artsObj, artsId);
            CoolTime(artsId);
        }
    }

    //クールタイム用
    void CoolTime(string artsId)
    {
        if (!coolTimes.ContainsKey(artsId))
        {
            //ここにそれぞれのクールタイムを入れる
            float timer = ArtsCoolTime.GetCoolTime(artsId, myEffectCount);

            //生成
            ArtsInstantManager.InstantArts(artsObj, artsId);
            coolTimes.Add(artsId, timer);
        }

        //所持しているid
        collectionKey = new List<string>(coolTimes.Keys);

        //タイマーの処理
        foreach (var key in collectionKey)
        {
            coolTimes[key] -= Time.deltaTime;
            if (coolTimes[key] > 0) coolTimes.Remove(key);
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
