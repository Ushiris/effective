using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id09_Arrow : MonoBehaviour
{
    [SerializeField] float force = 5f;
    [SerializeField] float maxForce = 30f;
    [SerializeField] GameObject arrowParticleObj;

    [SerializeField] GameObject chargeParticleObj;
    [SerializeField] float count = 10;
    float particleCount;

    GameObject tmpChargeParticleObj;
    float power;
    bool isStart;

    ParticleSystem.MainModule arrowParticle;

    ParticleSystem.EmissionModule chargeParticle;

    // Start is called before the first frame update
    void Start()
    {
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
                    particleCount += count * Time.deltaTime;
                    chargeParticle.rateOverTime = new ParticleSystem.MinMaxCurve(particleCount);
                }
                isStart = true;
            }
            else if (Input.GetMouseButtonUp(0) && isStart)
            {
                //矢を放つ
                arrowParticle.startSpeed = power;
                Instantiate(arrowParticleObj, transform);

                //チャージのパーティクルを消す
                Destroy(tmpChargeParticleObj);
            }
        }

        //オブジェクトを消す
        if (transform.childCount == 0) Destroy(gameObject);
    }
}
