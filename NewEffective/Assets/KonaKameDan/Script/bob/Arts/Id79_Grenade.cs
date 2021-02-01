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

    bool isStart = true;

    ArtsStatus artsStatus;
    ParticleHitPlayExplosion particleHitPlay;


    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();
        transform.parent = null;

        //エフェクトの所持数を代入
        explosionCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Explosion);
        flyCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Fly);

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

        var timer = Arts_Process.TimeAction(gameObject, slidingThroughLostTime);
        timer.LapEvent = () => { Lost(grenade); };

        //SE
        Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id79_Grenade_first, transform.position, artsStatus);
    }

    // Update is called once per frame
    void Update()
    {
        if(particleHitPlay != null)
        {
            //爆弾を破壊
            if (particleHitPlay.isTrigger == true)
            {
                //SE
                Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id047_PingPong_third,grenade.transform.position, artsStatus);

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
            Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id047_PingPong_third,grenade.transform.position, artsStatus);

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
