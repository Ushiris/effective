using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id027_AnnihilationRay2 : MonoBehaviour
{
    [SerializeField] GameObject eventTriggerObj;
    [SerializeField] GameObject infernoParticle;
    [SerializeField] float speedEvent = 30f;
    [SerializeField] float rollSpeed = 50f;
    [SerializeField] float waitTime = 1f;
    [SerializeField] float explosionWaitTime = 1f;
    [SerializeField] float defaultDamage = 2f;

    [Header("射撃のスタック数に応じてたされる数")]
    [SerializeField] float plusDamageShot = 0.02f;

    [Header("爆発のスタック数に応じてたされる数")]
    [SerializeField] float plusDamageExplosion = 0.02f;

    //エフェクトの所持数用
    int shotCount = 0;
    int explosionCount = 0;

    float time;
    bool isPlaySe;
    StopWatch timer;
    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //エフェクトの所持数を代入
        shotCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Shot);
        explosionCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Explosion);

        //ダメージの計算
        var fixDamage = (plusDamageShot * (float)shotCount) + (plusDamageExplosion * (float)explosionCount);
        var damage = defaultDamage + fixDamage;

        timer = gameObject.AddComponent<StopWatch>();
        timer.LapTime = waitTime;

        timer.LapEvent = () =>
        {
            //イベント設置用の弾丸生成
            var obj = Instantiate(eventTriggerObj, transform.GetChild(0).transform);
            obj.transform.parent = null;

            var advance = obj.GetComponent<ObjAdvance>();
            advance.speed = speedEvent;

            //爆発パーティクルの準備
            var hit = Arts_Process.SetParticleHitPlay
            (
                obj,
                infernoParticle,
                obj.transform,
                artsStatus,
                mode: ParticleHitPlayExplosion.Mode.My,
                isAllHit: true,
                isSelf: true
            );

            //〇〇秒遅れて爆発を再生
            var wait= obj.AddComponent<StopWatch>();
            wait.LapTime = explosionWaitTime;
            wait.LapEvent = () =>
            {
                if (advance.hitObj != null)
                {
                    hit.OnSelfExplosion(advance.hitObj);

                    //ダメージ
                    var particleHit = Arts_Process.SetParticleZoneDamageProcess(obj);
                    Arts_Process.ZoneDamage(particleHit, artsStatus, damage, true);

                    //se
                    Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id047_PingPong_third, obj.transform.position, artsStatus);
                }
                wait.enabled = false;
            };

            Destroy(obj, 3f);
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaySe)
        {
            // SE
            Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id027_annihilationRay_first, transform.position, artsStatus);
            isPlaySe = false;
        }
        //回転
        var step = rollSpeed * Time.deltaTime;
        var endRoll = Quaternion.Euler(0, 100, 0);
        transform.localRotation =
            Quaternion.RotateTowards(transform.localRotation, endRoll, step);

        if (transform.localRotation == endRoll)
        {
            Destroy(gameObject);
        }
    }
}
