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
    [SerializeField] ArtsInstantParticleView artsInstantParticleView;
    MyEffectCount myEffectCount;
    string tmpArtsId;
    string artsId;

    Dictionary<string, float> coolTimes = new Dictionary<string, float>();
    List<string> collectionKey = new List<string>();

    bool isEntryTrigger;
    bool isCoolTimeUI;

    public bool isArtsInstantLock { get; set; } = false;

    //デバッグ用
    public static bool isDebugCoolTime;
    public static float debugCoolTime;

    // Start is called before the first frame update
    void Start()
    {
        myEffectCount = artsObj.GetComponent<MyEffectCount>();
    }

    // Update is called once per frame
    void Update()
    {
        ArtsInstant();
    }

    void ArtsInstant()
    {
        //ArtsIDの取得
        artsId = ArtsInstantManager.SelectArts(MyArtsDeck.GetSelectArtsDeck.id, debugNum);

        //新しくArtsをセットし、それが前のArtsと違う場合のフラグ
        if (UI_Manager.ArtsEntryTrigger() && UI_Manager.GetIsEffectFusionUI_ChoiceActive) isEntryTrigger = true;
        if (isEntryTrigger && tmpArtsId != artsId)
        {
            tmpArtsId = artsId;
            isEntryTrigger = false;
            isCoolTimeUI = true;
        }

        //Artsを放つ
        if (OnTrigger())
        {
            GetEffectCount();                           //所持しているエフェクトを取得            
            if (artsId != "0") StartCoolTime(artsId);   //クールタイムが0の時Artsを放つ
            isCoolTimeUI = true;                        //UIを変更するフラグ
        }
        else CoolTime();                                //クールタイムの処理

        //クールタイムUIの処理フラグが立った場合UIをリセットする
        if (isCoolTimeUI)
        {
            isCoolTimeUI = SetCoolTimeUI();
        }
    }

    //クールタイム生成
    void StartCoolTime(string artsId)
    {
        if (!coolTimes.ContainsKey(artsId))
        {
            artsInstantParticleView.SetParticlePlay();

            //ここにそれぞれのクールタイムを入れる
            float timer;
            if (!isDebugCoolTime) timer = ArtsCoolTime.GetCoolTime(artsId, myEffectCount);
            else timer = debugCoolTime;

            //生成
            ArtsInstantManager.InstantArts(artsObj, artsId);
            coolTimes.Add(artsId, timer);
        }
    }

    //クールタイムの処理
    void CoolTime()
    {
        //所持しているid
        collectionKey = new List<string>(coolTimes.Keys);

        //タイマーの処理
        foreach (var key in collectionKey)
        {
            coolTimes[key] -= Time.deltaTime;
            //DebugLogger.Log("ID: "+key+" クールタイム: " + coolTimes[key]);
            if (coolTimes[key] < 0) coolTimes.Remove(key);
        }
    }

    //UI
    bool SetCoolTimeUI()
    {
        bool isTrigger = true;
        string key = MyArtsDeck.GetSelectArtsDeck.id;   //ArtsIDの取得
        try
        {
            //ArtsのクールタイムのMax値を取得
            float maxTime = ArtsCoolTime.GetCoolTime(key, myEffectCount);

            //クールタイムが発生している場合
            if (coolTimes.ContainsKey(key))
            {
                //クールタイムの現在の時間とmax値をUIを処理するところに渡す
                ArtsCoolTime.SetCoolTimeUI(coolTimes[key], maxTime, key);
                isTrigger = false;
            }
            else ArtsCoolTime.SetCoolTimeUI(0, 1, key);
        }
        catch (System.ArgumentException)
        {
            ArtsCoolTime.SetCoolTimeUI(0, 1, key);
        }

        return isTrigger;
    }

    //所持しているエフェクトのスタック数を入れる
    void GetEffectCount()
    {
        foreach (var item in MainGameManager.GetPlEffectList)
        {
            myEffectCount.effectCount[(NameDefinition.EffectName)item.id] = item.count;
        }
    }

    //アーツを放つキー
    bool OnTrigger()
    {
        var on = Time.timeScale > 0.1f && Input.GetMouseButtonDown(0) && !UI_Manager.GetIsEffectFusionUI_ChoiceActive;
        return on && !isArtsInstantLock;
    }
}
