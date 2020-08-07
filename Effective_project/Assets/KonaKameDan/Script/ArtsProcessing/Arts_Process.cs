using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Arts_Process : MonoBehaviour
{
    /// <summary>
    /// その者にとっての敵レイヤー取得
    /// </summary>
    /// <param name="artsStatus">誰の放ったアーツか記録されているもの</param>
    /// <returns></returns>
    public static int GetVsLayerMask(ArtsStatus artsStatus)
    {
        var type = artsStatus.type;

        if (type == ArtsStatus.ParticleType.Player)
        {
            return LayerMask.GetMask("Enemy");
        }else return LayerMask.GetMask("Player");
    }

    /// <summary>
    /// EMPのレイヤーを引っ張ってくる
    /// </summary>
    /// <param name="artsStatus"></param>
    /// <returns></returns>
    public static int GetEMPLayerMask(ArtsStatus artsStatus)
    {
        var type = artsStatus.type;

        if (type == ArtsStatus.ParticleType.Enemy)
        {
            return LayerMask.NameToLayer("EnemyEMP");
        }
        else return LayerMask.NameToLayer("PlayerEMP");
    }

    /// <summary>
    /// Artsを放った人が同じであればtrueを返す
    /// 違う場合listに格納する
    /// </summary>
    /// <param name="objs">ArtsActiveObjクラスに登録されているもの</param>
    /// <param name="obj">確認する対象</param>
    /// <returns></returns>
    public static bool GetMyActiveArts(List<GameObject> objs, GameObject obj)
    {
        if (objs.Count != 0)
        {
            if (objs.Contains(obj))
            {
                return true;
            }
        }

        objs.Add(obj);
        return false;
    }

    /// <summary>
    /// その者にとっての一番近い敵の位置を返す
    /// </summary>
    /// <param name="artsStatus">誰の放ったアーツか記録されているもの</param>
    /// <returns></returns>
    public static GameObject GetNearTarget(ArtsStatus artsStatus)
    {
        var type = artsStatus.type;
        GameObject pl = PlayerManager.GetManager.GetPlObj;

        if (type == ArtsStatus.ParticleType.Player)
        {
            Vector3 v3 = pl.transform.position;
            return pl.GetComponentInChildren<EnemyFind>().GetNearEnemyPos(v3);
        }
        else return pl;
    }

    /// <summary>
    /// StopWatchをまとめたやつ
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="lostTime"></param>
    /// <returns></returns>
    public static StopWatch TimeAction(GameObject obj, float lostTime)
    {
        var timer = obj.AddComponent<StopWatch>();
        timer.LapTime = lostTime;
        return timer;
    }

    /// <summary>
    /// 自分が所持しているエフェクトの数を出す
    /// </summary>
    /// <param name="artsStatus">誰の放ったアーツか記録されているもの</param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static int GetEffectCount(ArtsStatus artsStatus, NameDefinition.EffectName name)
    {
        var ec = artsStatus.myEffectCount;
        return ec.effectCount[name] - 1;
    }

    /// <summary>
    /// 無敵状態にするかどうか
    /// </summary>
    /// <param name="artsStatus"></param>
    /// <param name="isActive"></param>
    public static void Invisible(ArtsStatus artsStatus, bool isActive)
    {
        var s = artsStatus.myObj.GetComponent<InvisibleModel>();

        if (s != null)
        {
            s.Invisible(isActive);
        }
    }

    /// <summary>
    /// myから見たtargetの方向を返す
    /// </summary>
    /// <param name="my"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static Quaternion GetLookRotation(Transform my,Transform target)
    {
        var aim = target.position - my.position;
        return Quaternion.LookRotation(aim, Vector3.forward);
    }

    /// <summary>
    /// NavMeshAgentをアタッチ及び取得
    /// </summary>
    /// <param name="obj">対象</param>
    /// <returns></returns>
    public static NavMeshAgent SetNavMeshAgent(GameObject obj)
    {
        var agent = obj.AddComponent<NavMeshAgent>();
        return agent;
    }

    /// <summary>
    /// 特定の種類のArtsを消す  
    /// </summary>
    /// <param name="obj">アタッチする対象</param>
    /// <param name="artsType">Artsの種類</param>
    public static void SetDestroyArtsZone(GameObject obj, List<ArtsStatus.ArtsType> artsTypes, List<ArtsStatus.ParticleType> particleTypes)
    {
        var daz = obj.AddComponent<DestroyArtsZone>();
        daz.artsTypes = new List<ArtsStatus.ArtsType>(artsTypes);
        daz.particleTypes = new List<ArtsStatus.ParticleType>(particleTypes);
    }

    /// <summary>
    /// 特定の種類のアーツを消すやつのステータス設定
    /// </summary>
    /// <param name="obj">アタッチする対象</param>
    /// <param name="artsType"></param>
    /// <param name="particleTypes"></param>
    public static void SetDestroyArtsZoneStatus(GameObject obj, List<ArtsStatus.ArtsType> artsType, List<ArtsStatus.ParticleType> particleTypes)
    {
        var dazs=obj.AddComponent<DestroyArtsZoneStatus>();
        dazs.artsTypes = new List<ArtsStatus.ArtsType>(artsType);
        dazs.particleTypes = new List<ArtsStatus.ParticleType>(particleTypes);
    }

    /// <summary>
    /// 周りの物を吹きとばす
    /// </summary>
    /// <param name="pos">中心</param>
    /// <param name="radius">範囲</param>
    /// <param name="layer">吹きとばすもののレイヤー</param>
    /// <param name="explosionForce">吹きとばす力</param>
    /// <param name="uppersModifier">持ち上げる力</param>
    public static void Impact(Vector3 pos, float radius, int layerMask,
                              float explosionForce = 10f, float uppersModifier = 8f)
    {
        Collider[] enemies;

        if (layerMask == 0) enemies = Physics.OverlapSphere(pos, radius);
        else enemies = Physics.OverlapSphere(pos, radius, layerMask);

        foreach (Collider hit in enemies)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, pos, radius, uppersModifier, ForceMode.Impulse);
            }
        }
    }

    /// <summary>
    /// ダメージ処理をアタッチする
    /// </summary>
    /// <param name="obj">ダメージ処理を付けたい相手</param>
    public static ParticleHit SetParticleDamageProcess(GameObject obj)
    {
        return obj.AddComponent<ParticleHit>();
    }

    /// <summary>
    /// ダメージの計算
    /// </summary>
    /// <param name="defaultDamage"></param>
    /// <param name="plusDamage"></param>
    /// <param name="effectCount"></param>
    /// <returns></returns>
    public static float GetDamage(float defaultDamage, float plusDamage ,int effectCount)
    {
        return defaultDamage + (plusDamage * (float)effectCount);
    }

    /// <summary>
    /// ダメージ代入
    /// </summary>
    /// <param name="hit">パーティクル</param>
    /// <param name="artsStatus">誰の放ったアーツか記録されているもの</param>
    /// <param name="hitDefaultDamage">固定ダメージ</param>
    /// <param name="status">ステータス</param>
    /// <param name="hitObjTag">当たる相手</param>
    public static void Damage(ParticleHit hit, ArtsStatus artsStatus, float hitDefaultDamage, bool status, bool isMapLayer = true)
    {
        hit.hitDamageDefault = hitDefaultDamage;
        hit.artsStatus = artsStatus;
        hit.isMapLayer = isMapLayer;
        if (status) hit.plusFormStatus = artsStatus.myStatus.status[Status.Name.STR];
        
    }

    /// <summary>
    /// マウスから出たレイがヒットしたものが〇〇タグだった場合オブジェクトを移動
    /// </summary>
    /// <param name="hitTag">タグ名</param>
    /// <param name="defaultPos">ヒットしなかった場合こっれを返す</param>
    /// <param name="hitLayer">レイヤー</param>
    /// <returns></returns>
    public static Vector3 GetMouseRayHitPos(Vector3 defaultPos, string hitTag = "Untagged", string hitLayer="Nothing")
    {
        int layerMask = 0;
        if (hitLayer != "Nothing") layerMask = LayerMask.GetMask(hitLayer);
        Vector3 mousePos = Input.mousePosition;
        mousePos.x = Mathf.Clamp(mousePos.x, 0f, Screen.width);
        mousePos.y = Mathf.Clamp(mousePos.y, 0f, Screen.height);

        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit = new RaycastHit();
        if (hitLayer != "Nothing")
        {
            if (Layer()) return hit.point;
        }
        else
        {
            if (NotLayer()) return hit.point;
        }
        return defaultPos;

        //ヒットしたレイヤーに指定がある場合
        bool Layer()
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                if (hit.collider.tag == hitTag)
                {
                    return true;
                }
            }
            return false;
        }

        //レイヤー指定なし
        bool NotLayer()
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == hitTag)
                {
                    return true;
                }
            }
            return false;
        }
    }

    /// <summary>
    /// パーティクルの出る数を変更する
    /// </summary>
    /// <param name="pse"></param>
    /// <param name="defaultBullet">固定数</param>
    /// <param name="addBullet">増える数</param>
    /// <param name="spreadCount">変動カウント</param>
    public static void SetBulletCount(ParticleSystem.EmissionModule pse, int defaultBullet, int addBullet, int spreadCount)
    {
        int bulletCount = defaultBullet + (addBullet * spreadCount);
        pse.rateOverTime = new ParticleSystem.MinMaxCurve(bulletCount);
    }

    /// <summary>
    /// インパクトシェーダーの透明度の取得とセット
    /// </summary>
    /// <param name="material"></param>
    /// <param name="defaultNum"></param>
    /// <returns></returns>
    public static float GetImpactShaderMaterialFade(Material material,float defaultNum)
    {
        material.SetFloat("Vector1_488B2355", defaultNum);
        return material.GetFloat("Vector1_488B2355");
    }

    /// <summary>
    /// Rippleシェーダーの初期化
    /// </summary>
    /// <param name="material"></param>
    public static void RippleShaderReset(Material material)
    {
        material.SetFloat("Vector1_62243949", 0);
    }

    /// <summary>
    /// Rippleシェーダーの広がる範囲を決める
    /// </summary>
    /// <param name="material"></param>
    /// <param name="dis"></param>
    public static void RippleShaderStart(Material material, float dis)
    {
        material.SetFloat("Vector1_62243949", dis);
    }

    /// <summary>
    /// サーチシェーダーの基準位置を決める
    /// </summary>
    /// <param name="material"></param>
    /// <param name="pos"></param>
    public static void SearchPosSet(Material material, Vector3 pos)
    {
        material.SetVector("Vector3_CB8F0829", pos);
    }

    /// <summary>
    /// サーチシェーダーの広がる範囲を決める
    /// </summary>
    /// <param name="material"></param>
    /// <param name="dis"></param>
    public static void SearchShaderStart(Material material,float dis)
    {
        material.SetFloat("Vector1_FCA7DA60", dis);
    }

    /// <summary>
    /// サーチシェーダーの位置をリセットする
    /// </summary>
    /// <param name="material"></param>
    public static void SearchShaderReset(Material material)
    {
        material.SetFloat("Vector1_FCA7DA60", 0);
    }

    /// <summary>
    /// 円周上のポジションを取る(円状に何かを並べるときに使う)
    /// </summary>
    /// <param name="count">取得する数</param>
    /// <param name="radius">半径</param>
    /// <param name="angle">角度</param>
    /// <returns></returns>
    public static List<Vector3> GetCirclePutPos(int count,float radius,float angle)
    {
        List<Vector3> pos = new List<Vector3>();
        for (int i = 0; i < count; i++)
        {

            float r = (angle / count) * i;
            r *= Mathf.Deg2Rad;
            pos.Add(new Vector3(radius * Mathf.Cos(r), 0f, radius * Mathf.Sin(r)));
        }
        return new List<Vector3>(pos);
    }

    /// <summary>
    /// 盾のレイヤーを決めるやつ(EnemyかPlayerか)
    /// </summary>
    /// <param name="artsStatus"></param>
    /// <param name="shieldObj"></param>
    public static void SetShieldLayer(ArtsStatus artsStatus,GameObject shieldObj)
    {
        var type = artsStatus.type;
        if (type == ArtsStatus.ParticleType.Player)
        {
            shieldObj.layer = LayerMask.NameToLayer("PlayerShield");
        }
        else
        {
            shieldObj.layer = LayerMask.NameToLayer("EnemyShield");
        }
    }

    /// <summary>
    /// 剣のレイヤーを決めるやつ(EnemyかPlayerか)
    /// </summary>
    /// <param name="artsStatus"></param>
    /// <returns></returns>
    public static void SetArtsLayerMask(ArtsStatus artsStatus,GameObject obj)
    {
        var type = artsStatus.type;
        if (type == ArtsStatus.ParticleType.Player)
        {
            obj.layer = LayerMask.NameToLayer("PlayerArts");
        }
        else
        {
            obj.layer = LayerMask.NameToLayer("EnemyArts");
        }
    }

    /// <summary>
    /// オブジェクトをぐるぐる回転させる
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="speed"></param>
    public static void ObjRoll(GameObject obj, float speed = 90f)
    {
        var move = new Vector3(0, 1, 0) * speed * Time.deltaTime;
        obj.transform.Rotate(move, Space.Self);
    }

    /// <summary>
    /// オブジェクトのサイズの初期化
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="siz"></param>
    public static void SetObjSiz(GameObject obj,Vector3 siz)
    {
        obj.transform.localScale = siz;
    }

    /// <summary>
    /// 指定しているサイズにオブジェクトのサイズが近しい場合真を返す
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="siz"></param>
    /// <returns></returns>
    public static bool ObjSizFlag(GameObject obj,Vector3 siz)
    {
        Vector3 v3 = obj.transform.localScale;
        float dis = Vector3.Distance(v3, siz);
        if (dis > 0.01f) return true;
        else return false;
    }

    /// <summary>
    /// オブジェクトのサイズを変更する動きをするスクリプトのアタッチ
    /// </summary>
    /// <param name="obj">アタッチするもの</param>
    /// <param name="defaultSiz">デフォルトのサイズ</param>
    /// <param name="maxSiz"></param>
    /// <param name="changeSizPos">変更する成分</param>
    /// <param name="sizChangeSpeed">変更速度</param>
    public static ObjSizChange SetAddObjSizChange
        (
        GameObject obj,Vector3 defaultSiz, Vector3 maxSiz,
        float sizChangeSpeed,
        ObjSizChange.SizChangeMode sizChangeMode
        )
    {
        var s = obj.AddComponent<ObjSizChange>();
        s.defaultSiz = defaultSiz;
        s.maxSiz = maxSiz;
        s.sizChangeSpeed = sizChangeSpeed;
        s.SetSizChangeMode = sizChangeMode;
        return s;
    }

    /// <summary>
    /// リキッドボディーを持っているオブジェクトに対して力を加え尾久化します
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="force"></param>
    public static void RbMomentMove(GameObject obj,float force)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.AddForce(obj.transform.forward * force, ForceMode.Impulse);
    }

    /// <summary>
    /// 複数のマテリアルが適応されているオブジェクトのマテリアルを変更する
    /// </summary>
    /// <param name="objects">オブジェクト</param>
    /// <param name="material">変更後のマテリアル</param>
    /// <param name="materialNum">マテリアルナンバー</param>
    public static void MaterialsChange(GameObject[] objects, Material material,int materialNum)
    {
        foreach(var obj in objects)
        {
            MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
            Material[] mats = meshRenderer.materials;
            mats[materialNum] = material;
            meshRenderer.materials = mats;
        }
    }

    /// <summary>
    /// 特定のオブジェクトの場所をマークするUIを生成する
    /// </summary>
    /// <param name="target">マークするオブジェクト</param>
    /// <param name="ui">生成するUI</param>
    /// <param name="parent">グループ</param>
    /// <param name="deleteTime">消す時間</param>
    public static void SearchMarkUiInstant(GameObject[] target,GameObject ui,Transform parent,float deleteTime)
    {
        foreach(var obj in target)
        {
            GameObject uiInstant = Instantiate(ui, parent);
            uiInstant.GetComponent<ItemSearch>().obj = obj;
            uiInstant.GetComponent<ItemSearch>().deleteTime = deleteTime;
        }
    }

    /// <summary>
    /// 生成するパーティクルの生成数を決めることができる
    /// 返り値はParticleSystem.EmissionModule
    /// </summary>
    /// <param name="lightGathersParticleObj">生成するパーティクル</param>
    /// <param name="startParticleCount">パーティクル数の初期値</param>
    /// <returns></returns>
    public static ParticleSystem.EmissionModule LightGathersParticleInstant(GameObject lightGathersParticleObj,float startParticleCount)
    {
        ParticleSystem.EmissionModule psem = 
            lightGathersParticleObj.GetComponent<ParticleSystem>().emission;
        psem.rateOverTime = new ParticleSystem.MinMaxCurve(startParticleCount);
        return psem;
    }

    /// <summary>
    /// GameObjectからParticleSystem.MainModuleに変える
    /// </summary>
    /// <param name="obj">パーティクルのオブジェクト</param>
    /// <returns></returns>
    public static ParticleSystem.MainModule ObjCastPS_MainModule(GameObject obj)
    {
        ParticleSystem ps = obj.GetComponent<ParticleSystem>();
        return ps.main;
    }

    /// <summary>
    /// 奇跡を表示する
    /// </summary>
    /// <param name="objs">置くもの</param>
    /// <param name="space">幅</param>
    /// <param name="v0">力の向き</param>
    /// <returns></returns>
    public static List<GameObject> Miracle(List<GameObject> objs, float space, Vector3 v0)
    {
        int count = 0;
        foreach(var obj in objs)
        {
            var t = count * space;
            var x = t * v0.x;
            var z = t * v0.z;
            var y = (v0.y * t) - 0.5f * (-Physics.gravity.y) * Mathf.Pow(t, 2.0f);
            obj.transform.localPosition = new Vector3(x, y, z);

            count++;
        }

        return new List<GameObject>(objs);
    }

    /// <summary>
    /// ホーミングの処理
    /// </summary>
    /// <param name="particleSystem"></param>
    /// <param name="force">力</param>
    public static void HomingParticle(ParticleSystem particleSystem, GameObject target, float force)
    {
        ParticleSystem.MainModule particleSystemMainModule;
        particleSystemMainModule = particleSystem.main;

        //パーティクルの最大値
        int maxParticles = particleSystemMainModule.maxParticles;

        //全てのパーティクルを入れる
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[maxParticles];

        //パーティクルを取得
        particleSystem.GetParticles(particles);

        //速度の計算
        float forceDeltaTime = force * Time.deltaTime;

        //ターゲットの座標
        Vector3 targetTransformedPosition = target.transform.position;

        //現在出ているパーティクルの数
        int particleCount = particleSystem.particleCount;

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
        particleSystem.SetParticles(particles, particleCount);
    }

    /// <summary>
    /// 当たり判定作成
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="range"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static GameObject SphereColliderObjInstant(Transform parent, float range=10f,string name="HitArea")
    {
        GameObject area = Instantiate(new GameObject("HitArea"), parent);
        var c = area.AddComponent<SphereCollider>();
        c.isTrigger = true;
        var rb = area.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
        area.transform.localScale = Vector3.one * range;

        return area;
    }
}
