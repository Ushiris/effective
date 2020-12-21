using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id79_Grenade : MonoBehaviour
{
    [SerializeField] GameObject trajectoryObj;
    [SerializeField] GameObject grenadeObj;
    [SerializeField] GameObject bulletObj;
    [SerializeField] Vector3 v0 = new Vector3(0, 5, 7);
    [SerializeField] float trajectoryCount = 10;
    [SerializeField] float trajectorySpace = 0.1f;
    [SerializeField] float lostTime = 1;
    [SerializeField] float slidingThroughLostTime = 5;
    [SerializeField] float defaultDamage = 1.2f;

    [Header("爆発のスタック数に応じてたされる数")]
    [SerializeField] float plusDamage = 0.02f;

    [Header("飛翔のスタック数に応じてたされる数")]
    [SerializeField] float addForth = 0.5f;

    //エフェクトの所持数用
    int explosionCount = 0;
    int flyCount = 0;
    float damage;

    Vector3 pos;
    GameObject grenade;
    List<GameObject> miracles = new List<GameObject>();

    bool isStart = true;

    ArtsStatus artsStatus;
    ParticleHitPlayExplosion particleHitPlay;


    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //エフェクトの所持数を代入
        explosionCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Explosion);
        flyCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Fly);

        for (int i = 0; i < trajectoryCount; i++)
        {
            miracles.Add(Instantiate(trajectoryObj, transform));
        }

        //軌跡を生成
        miracles = Arts_Process.Trajectory(miracles, trajectorySpace, v0);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && isStart)
        {
            transform.parent = null;

            //Grenade生成
            grenade = Instantiate(bulletObj, transform);

            //grenadeを投げる
            Rigidbody jumpCubeRb = grenade.GetComponent<Rigidbody>();
            jumpCubeRb.AddRelativeFor​​ce(v0, ForceMode.VelocityChange);

            Damage(grenade);

            //爆発するエフェクトのセット
            particleHitPlay =
                Arts_Process.SetParticleHitPlay(grenade, grenadeObj, transform, artsStatus, lostTime, ParticleHitPlayExplosion.Mode.My, true);
            isStart = false;

            for (int i = 0; i < trajectoryCount; i++)
            {
                Destroy(miracles[i]);
            }

            var timer = Arts_Process.TimeAction(gameObject, slidingThroughLostTime);
            timer.LapEvent = () => { Lost(grenade); };

            //SE
            SE_Manager.SePlay(SE_Manager.SE_NAME.Id79_Grenade_first);
        }

        if(particleHitPlay != null)
        {
            //爆弾を破壊
            if (particleHitPlay.isTrigger == true)
            {
                //SE
                var se = SE_Manager.Se3dPlay(SE_Manager.SE_NAME.Id047_PingPong_third);
                SE_Manager.Se3dMove(grenade.transform.position, se);

                Destroy(grenade);
                Destroy(gameObject, 3);
            }
        }
    }
    //一定時間たったら爆発する
    void Lost(GameObject obj)
    {
        if (obj != null)
        {
            pos = obj.transform.position;
            Destroy(obj);
            var explosion = Instantiate(grenadeObj, pos, Quaternion.identity);

            //SE
            var se = SE_Manager.Se3dPlay(SE_Manager.SE_NAME.Id047_PingPong_third);
            SE_Manager.Se3dMove(grenade.transform.position, se);

            Destroy(gameObject, 2);
        }
    }

    void Damage(GameObject obj)
    {
        //ダメージ
        var objDamage = Arts_Process.SetParticleDamageProcess(obj);

        //ダメージの計算
        damage = defaultDamage + (plusDamage * (float)explosionCount);

        //ダメージ処理
        Arts_Process.Damage(objDamage, artsStatus, damage, true);
    }
}
