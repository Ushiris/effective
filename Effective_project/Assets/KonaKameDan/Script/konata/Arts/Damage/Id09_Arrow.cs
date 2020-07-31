using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id09_Arrow : MonoBehaviour
{
    [Header("矢のステータス")]
    [SerializeField] float force = 5f;
    [SerializeField] float maxForce = 30f;
    [SerializeField] GameObject arrowParticleObj;

    [Header("チャージのエフェクト")]
    [SerializeField] GameObject chargeParticleObj;
    [SerializeField] float chargeSpeed = 10;
    float particleCount;

    [Header("ダメージ")]
    [SerializeField] float defaultDamage = 0.8f;

    [Header("射撃のスタック数に応じてたされる数")]
    [SerializeField] float plusDamage = 0.01f;

    [Header("チャージ短縮時間")]
    [SerializeField] float chargeTimeDown = 0.1f;

    GameObject tmpChargeParticleObj;
    float power;
    bool isStart;

    ParticleSystem.MainModule arrowParticle;

    ParticleSystem.EmissionModule chargeParticle;

    int shotCount;
    int flyCount;
    float damage;

    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //エフェクトの所持数を代入
        shotCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Shot);
        flyCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Fly);

        //チャージ時間短縮
        chargeSpeed += chargeTimeDown * (float)flyCount;
        force += chargeTimeDown * (float)flyCount;

        // ダメージの計算
        damage = Arts_Process.GetDamage(defaultDamage, plusDamage, shotCount);

        //矢の初期化
        arrowParticle = Arts_Process.ObjCastPS_MainModule(arrowParticleObj);
        arrowParticle.startSpeed = power;

        //チャージエフェクトの初期化
        tmpChargeParticleObj = Instantiate(chargeParticleObj, transform);
        chargeParticle =
            Arts_Process.LightGathersParticleInstant(tmpChargeParticleObj, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (tmpChargeParticleObj != null)
        {
            if (Input.GetMouseButton(0))
            {
                if (maxForce > power)
                {
                    //矢の力をためる
                    power += force * Time.deltaTime;

                    //チャージの数の変化
                    particleCount += chargeSpeed * Time.deltaTime;
                    chargeParticle.rateOverTime = new ParticleSystem.MinMaxCurve(particleCount);
                }
                isStart = true;
            }
            else if (Input.GetMouseButtonUp(0) && isStart)
            {
                //矢を放つ
                arrowParticle.startSpeed = power;
                var obj = Instantiate(arrowParticleObj, transform);

                //ダメージ
                var hit = Arts_Process.SetParticleDamageProcess(obj);
                //ダメージ処理
                Arts_Process.Damage(hit, artsStatus, damage, true);

                //チャージのパーティクルを消す
                Destroy(tmpChargeParticleObj);
            }
        }

        //オブジェクトを消す
        if (transform.childCount == 0) Destroy(gameObject);
    }
}
