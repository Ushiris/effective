using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id18_ChargeSlash : MonoBehaviour
{
    [SerializeField] GameObject slashParticleObj;

    [SerializeField] GameObject chargeParticleObj;
    [SerializeField] float chargeSpeed = 10;
    [SerializeField] float maxCharge = 100f;
    float particleCount;

    bool isStart;
    GameObject tmpChargeParticleObj;
    ParticleSystem.EmissionModule chargeParticle;

    ArtsStatus artsStatus;
    ParticleHit slashDamage;

    // Start is called before the first frame update
    void Start()
    {
        //チャージパーティクルの初期化
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
                if (maxCharge > particleCount)
                {
                    //チャージの数の変化
                    particleCount += chargeSpeed * Time.deltaTime;
                    chargeParticle.rateOverTime = new ParticleSystem.MinMaxCurve(particleCount);
                }
                isStart = true;
            }
            else if (Input.GetMouseButtonUp(0) && isStart)
            {
                //切り裂く
                var obj = Instantiate(slashParticleObj, transform);

                Damage(obj);

                //チャージのパーティクルを消す
                Destroy(tmpChargeParticleObj);
            }
        }

        //オブジェクトを消す
        if (transform.childCount == 0) Destroy(gameObject);
    }

    void Damage(GameObject obj)
    {
        //当たり判定セット
        slashDamage = Arts_Process.SetParticleDamageProcess(obj);
    }
}
