using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id045_Hounds : MonoBehaviour
{
    [SerializeField] GameObject homingParticle;
    [SerializeField] float force = 10f;
    [SerializeField] float defaultDamage = 0.1f;

    GameObject target;
    GameObject homingParticleObj;

    ParticleHit homingDamage;
    new ParticleSystem particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        homingParticleObj = Instantiate(homingParticle, transform);
        particleSystem = homingParticleObj.GetComponent<ParticleSystem>();

        //ダメージ
        Arts_Process.SetParticleDamageProcess(homingParticleObj);
        homingDamage = homingParticleObj.GetComponent<ParticleHit>();

        //敵のポジションを持ってくる
        target = Arts_Process.GetEnemyTarget();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //ホーミング処理
        if (target != null && homingParticleObj != null)
        {
            Arts_Process.HomingParticle(particleSystem, target, force);
        }

        //ダメージ処理
        Arts_Process.Damage(homingDamage, defaultDamage, true);

        //オブジェクトを消す
        if (transform.childCount == 0) Destroy(gameObject);
    }
}
