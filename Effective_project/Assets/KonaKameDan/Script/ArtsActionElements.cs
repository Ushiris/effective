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

[System.Serializable]
public class ParticleSystemDictionary : Serialize.TableBase<string, ParticleSystem, Name2ParticleSystem> { }

[System.Serializable]
public class Name2ParticleSystem : Serialize.KeyAndValue<string, ParticleSystem>
{
    public Name2ParticleSystem(string key, ParticleSystem value) : base(key, value) { }
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

    [SerializeField]
    ParticleSystemDictionary particleSystem;


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

    /// <summary>
    /// 一番近い敵の位置を持ってくる
    /// </summary>
    /// <returns></returns>
    public Transform GetEnemyTarget()
    {
        Vector3 v3 = player.transform.position;
        return GetComponent<EnemyFind>().GetNearEnemyPos(v3).transform;
    }

    /// <summary>
    /// ホーミング弾の処理
    /// </summary>
    /// <param name="name"></param>
    public void Homing(string name)
    {
        ParticleSystem ps = particleSystem.GetTable()[name];
        ParticleSystem.MainModule psm = ps.main;

        float force = 10.0f;

        //パーティクルの最大値
        int maxParticles = psm.maxParticles;

        //全てのパーティクルを入れる
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[maxParticles];

        //パーティクルを取得
        ps.GetParticles(particles);

        //速度の計算
        float forceDeltaTime = force * Time.deltaTime;

        //ターゲットの座標
        Vector3 targetTransformedPosition = GetEnemyTarget().position;

        //現在出ているパーティクルの数
        int particleCount = ps.particleCount;

        //パーティクル全ての位置をターゲットに向かわせる
        for (int i = 0; i < particleCount; i++)
        {
            //方向
            Vector3 directionToTarget = Vector3.Normalize(targetTransformedPosition - particles[i].position);
            //速度
            Vector3 seekForce = directionToTarget * forceDeltaTime;
            //パーティクルに代入
            particles[i].velocity += seekForce;
        }

        //パーティクルに反映
        ps.SetParticles(particles, particleCount);
    }
}
