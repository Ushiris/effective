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
    [SerializeField]
    GameObject player;

    //初期化関数。一度だけ処理されます
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public void Init()
    {
        if (isInit) return;
        if (player == null) player = GameObject.Find("Player");

        //例："何もしない"の定義
        Actions.Add("Brank", Brank);
        //ここで上記の例を元に関数名と関数の紐づけを行ってください

        isInit = true;
    }

    //例："何もしない"をするスクリプト
    public void Brank(GameObject arts) { }

    //例：prefabに関する操作のチュートリアル
    public void HogeMove(GameObject arts)
    {
        //インスペクタ上で登録されているであろう"Hoge"というプレハブをインスタンス化します
        //これは矢をインスタンス化したりする場合に有効で効率的な手段と言えます
        //Summon()はオーバーライドされていて、変数の型を明示的に書くのは面倒です。こういった場合はvarで推論しましょう
        var hoge_instance = Summon("Hoge");

        //artsの子として登録します
        SetParent(arts.transform, hoge_instance.transform);
        
        //前に飛ばす
        hoge_instance.GetComponent<Rigidbody>().AddForce(hoge_instance.transform.forward);
    }

    //例：player等の情報を参照する
    public void KnockBack_ex(GameObject arts)
    {
        //反動でノックバックするイメージの関数
        //プレイヤーは参照する機会が多いと予測されているので、予めメンバ変数にキャッシュされています。
        player.GetComponent<Rigidbody>().AddForce(-player.transform.forward + new Vector3(0, 100, 0));
    }

      /************************************/
     /* ここにキーワード動作を書いてけろ */
    /************************************/


    //インスタンス化はよく使うので関数を用意しておきました。
    private GameObject Summon(string name)
    {
        return Instantiate(prefabs.GetTable()[name]);
    }

    //一気に何個か作りたいとき用
    private List<GameObject> Summon(string name,int amount)
    {
        var instance = new List<GameObject>();
        for (uint i = 0; i < amount; i++)
        {
            instance.Add(Summon(name));
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
