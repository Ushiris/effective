using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PrefabDictionary : Serialize.TableBase<string, GameObject, Name2Prefab> { }

[System.Serializable]
public class Name2Prefab : Serialize.KeyAndValue<string, GameObject>
{
    public Name2Prefab(string key, GameObject value) : base(key, value){ }
}

public class ArtsActionElements : SingletonMonoBehaviour<ArtsActionElements>
{
    public delegate void ArtsAction(GameObject arts);
    public Dictionary<string, ArtsAction> Actions { get; private set; }
    bool isInit = false;

    [SerializeField]
    PrefabDictionary prefabs;

    //一度だけ処理されます。
    public void Init()
    {
        if (isInit) return;

        //init dictionary
        Actions.Add("Brank", Brank);

        isInit = true;
    }

    //"何もしない"をするスクリプト
    public void Brank(GameObject arts) { }

    //ここにキーワード動作を書いてけろ
}
