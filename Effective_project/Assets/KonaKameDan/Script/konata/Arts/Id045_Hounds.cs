using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id045_Hounds : MonoBehaviour
{
    [SerializeField] GameObject homingParticle;
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

    // Start is called before the first frame update
    void Start()
    {
        homingParticleObj = Instantiate(homingParticle, transform);
        particleSystem = homingParticleObj.GetComponent<ParticleSystem>();


        //エフェクトの所持数を代入
        var ec = GetComponentInParent<MyEffectCount>();
        shotCount = ec.effectCount[NameDefinition.EffectName.Shot] - 1;
        spreadCount = ec.effectCount[NameDefinition.EffectName.Spread] - 1;


        //ダメージ
        Arts_Process.SetParticleDamageProcess(homingParticleObj);
        homingDamage = homingParticleObj.GetComponent<ParticleHit>();

        //ダメージの計算
        damage = defaultDamage + (plusDamage * (float)shotCount);

        //弾数を増やす
        var pse = particleSystem.emission;
        Arts_Process.SetBulletCount(pse, defaultBullet, addBullet, spreadCount);


        //敵のポジションを持ってくる
        target = Arts_Process.GetEnemyTarget();

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
        Arts_Process.Damage(homingDamage, defaultDamage, true);

        //オブジェクトを消す
        if (transform.childCount == 0) Destroy(gameObject);
    }
}
