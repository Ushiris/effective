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
        //インスペクタ上で登録されているであろう"Hoge"というプレハブをインスタンス化します
        //これは例えば矢をインスタンス化したりする場合に有効です。
        GameObject hoge_instance = Summon("Hoge");

        //artsの子として登録します
        SetParent(arts.transform, hoge_instance.transform);

        //前に飛ばす
        hoge_instance.GetComponent<Rigidbody>().AddForce(hoge_instance.transform.forward);
    }

    //インスタンス化はよく使うので関数を用意しておきました。
    private GameObject Summon(string name)
    {
        return Instantiate(prefabs.GetTable()[name]);
    }

    private List<GameObject> Summon(string name,int amount)
    {
        var instance = new List<GameObject>();
        for (uint i = 0; i < amount; i++)
        {
            instance.Add( Summon(name));
        }
        return instance;
    }

    //アーツを親として登録します。座標がアーツの地点になるので注意。
    private void SetParent(Transform arts_tr,Transform instance_tr)
    {
        instance_tr.parent = arts_tr;
        instance_tr.localPosition = Vector3.zero;
    }
}
