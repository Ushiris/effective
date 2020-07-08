using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Arts_Process : MonoBehaviour
{
    /// <summary>
    /// 一番近い敵の位置を返す
    /// </summary>
    /// <returns></returns>
    public static GameObject GetEnemyTarget()
    {
        GameObject pl = PlayerManager.GetManager.GetPlObj;
        Vector3 v3 = pl.transform.position;
        return pl.GetComponentInChildren<EnemyFind>().GetNearEnemyPos(v3);
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
    /// 周りの物を吹きとばす
    /// </summary>
    /// <param name="pos">中心</param>
    /// <param name="radius">範囲</param>
    /// <param name="layer">吹きとばすもののレイヤー</param>
    /// <param name="explosionForce">吹きとばす力</param>
    /// <param name="uppersModifier">持ち上げる力</param>
    public static void Impact(Vector3 pos, float radius, string layer = "Nothing",
                              float explosionForce = 10f, float uppersModifier = 8f)
    {
        Collider[] enemies;
        int layerMask;

        if (layer == "Nothing") enemies = Physics.OverlapSphere(pos, radius);
        else
        {
            layerMask = LayerMask.GetMask(layer);
            enemies = Physics.OverlapSphere(pos, radius, layerMask);
        }

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
    public static void SetParticleDamageProcess(GameObject obj)
    {
        obj.AddComponent<ParticleHit>();
    }

    /// <summary>
    /// ダメージ代入
    /// </summary>
    /// <param name="hit">パーティクル</param>
    /// <param name="hitDefaultDamage">固定ダメージ</param>
    /// <param name="status">ステータス</param>
    /// <param name="hitObjTag">当たる相手</param>
    public static void Damage(ParticleHit hit, float hitDefaultDamage, bool status, string hitObjTag = "Enemy")
    {
        hit.hitDamageDefault = hitDefaultDamage;
        if (status) hit.plusFormStatus = PlayerManager.GetManager.GetPlObj.GetComponent<Status>().status[Status.Name.STR];
        hit.hitObjTag = hitObjTag;
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
        GameObject obj,
        Vector3 defaultSiz, Vector3 maxSiz, Vector3 changeSizPos,
        float sizChangeSpeed,
        ObjSizChange.SizChangeMode sizChangeMode
        )
    {
        var s = obj.AddComponent<ObjSizChange>();
        s.defaultSiz = defaultSiz;
        s.maxSiz = maxSiz;
        s.changeSizPos = changeSizPos;
        s.sizChangeSpeed = sizChangeSpeed;
        s.SetSizChangeMode = sizChangeMode;
        return s;
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
}
