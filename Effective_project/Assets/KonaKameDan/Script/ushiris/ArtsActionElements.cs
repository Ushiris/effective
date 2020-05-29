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

        //例："何もしない"の定義
        Actions.Add("Brank", Brank);

        isInit = true;
    }
    //ここにキーワード動作を書いてけろ

    //"何もしない"をするスクリプト
    public void Brank(GameObject arts) { }

    //例：prefab(Hoge)に関する操作
    public void HogeMove(GameObject arts)
    {
        //インスペクタ上で登録されているであろう"Hoge"というプレハブをインスタンス化します。
        GameObject hoge_instance = CreateInstance("Hoge");

        //artsの子として登録します。プレハブのデフォルト座標等の設定に気を付けてください。
        hoge_instance.transform.parent = arts.transform;
        hoge_instance.transform.position = Vector3.zero;//artsの座標に移動
    }

    //インスタンス化はよく使うので関数を用意しておきました。
    private GameObject CreateInstance(string name)
    {
        return Instantiate(prefabs.GetTable()[name]);
    }

}
