using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id045_Hounds : MonoBehaviour
{
    [SerializeField] GameObject homingParticle;
    [SerializeField] GameObject[] startWaveParticle;
    [SerializeField] float force = 10f;
    [SerializeField] float defaultDamage = 0.1f;
    [SerializeField] int defaultBullet = 10;

    [Header("射撃のスタック数に応じてたされる数")]
    [SerializeField] float plusDamage = 0.05f;

    [Header("拡散のスタック数に応じてたされる数")]
    [SerializeField] int addBullet = 1;

    //エフェクトの所持数用
    int shotCount = 0;
    int spreadCount = 0;
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

        //初手のエフェクト
        for (int i = 0; i < startWaveParticle.Length; i++)
        {
            Instantiate(startWaveParticle[i], transform);
        }

        //パーティクル生成
        homingParticleObj = Instantiate(homingParticle, transform);
        particleSystem = homingParticleObj.GetComponent<ParticleSystem>();


        //エフェクトの所持数を代入
        shotCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Shot);
        spreadCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Spread);


        //ダメージ
        homingDamage = Arts_Process.SetParticleDamageProcess(homingParticleObj);

        //ダメージの計算
        damage = defaultDamage + (plusDamage * (float)shotCount);

        //ダメージ処理
        Arts_Process.Damage(homingDamage, artsStatus, damage, true);

        //弾数を増やす
        var pse = particleSystem.emission;
        Arts_Process.SetBulletCount(pse, defaultBullet, addBullet, spreadCount);


        //敵のポジションを持ってくる
        target = Arts_Process.GetNearTarget(artsStatus);

        //SE
        SE_Manager.SePlay(SE_Manager.SE_NAME.Id045_Hounds_first);
        SE_Manager.SePlay(SE_Manager.SE_NAME.Id045_Hounds_second);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //ホーミング処理
        if (target != null && homingParticleObj != null)
        {
            Arts_Process.HomingParticle(particleSystem, target, force);
        }

        //オブジェクトを消す
        if (transform.childCount == 0) Destroy(gameObject);
    }
}
