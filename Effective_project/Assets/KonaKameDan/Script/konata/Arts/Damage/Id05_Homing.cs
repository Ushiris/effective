using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id05_Homing : MonoBehaviour
{
    [SerializeField] GameObject homingParticle;
    [SerializeField] float force = 10f;
    [SerializeField] float defaultDamage = 1f;

    [Header("射撃のスタック数に応じてたされる数")]
    [SerializeField] float plusDamage = 0.05f;

    //エフェクトの所持数用
    int shotCount = 0;
    float damage;

    GameObject target;
    GameObject homingParticleObj;

    ParticleHit homingDamage;
    new ParticleSystem particleSystem;
    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //パーティクル生成
        homingParticleObj = Instantiate(homingParticle, transform);
        particleSystem = homingParticleObj.GetComponent<ParticleSystem>();


        //エフェクトの所持数を代入
        shotCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Shot);


        //ダメージ
        homingDamage = Arts_Process.SetParticleDamageProcess(homingParticleObj);

        //ダメージの計算
        damage = defaultDamage + (plusDamage * (float)shotCount);

        //敵のポジションを持ってくる
        target = Arts_Process.GetNearTarget(artsStatus);

        //SE
        SE_Manager.SePlay(SE_Manager.SE_NAME.Shot);
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
        Arts_Process.Damage(homingDamage, artsStatus, damage, true);

        //オブジェクトを消す
        if (transform.childCount == 0) Destroy(gameObject);
    }
}
