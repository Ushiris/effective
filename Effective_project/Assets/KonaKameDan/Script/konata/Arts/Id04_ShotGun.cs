using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id04_ShotGun : MonoBehaviour
{
    [SerializeField] GameObject shotGunParticle;
    [SerializeField] float defaultDamage = 0.2f;
    [SerializeField] int defaultBullet = 10;

    [Header("射撃のスタック数に応じてたされる数")]
    [SerializeField] float plusDamage = 0.05f;

    [Header("拡散のスタック数に応じてたされる数")]
    [SerializeField] int addBullet = 1;

    int shotCount = 0;
    int spreadCount = 0;
    float damage;

    GameObject shotGunParticleObj;
    ParticleHit homingDamage;

    // Start is called before the first frame update
    void Start()
    {
        shotGunParticleObj = Instantiate(shotGunParticle, transform);

        //エフェクトの所持数を代入
        var ec = GetComponentInParent<MyEffectCount>();
        shotCount = ec.effectCount[NameDefinition.EffectName.Shot] - 1;
        spreadCount = ec.effectCount[NameDefinition.EffectName.Spread] - 1;

        //ダメージ
        Arts_Process.SetParticleDamageProcess(shotGunParticleObj);
        homingDamage = shotGunParticleObj.GetComponent<ParticleHit>();

        //ダメージの計算
        damage = defaultDamage + (plusDamage * (float)shotCount);

        //弾数を増やす
        var ps = shotGunParticleObj.GetComponent<ParticleSystem>();
        var pse = ps.emission;
        int bulletCount = defaultBullet + (addBullet * spreadCount);
        pse.rateOverTime = new ParticleSystem.MinMaxCurve(bulletCount);

        //SE
        SE_Manager.SePlay(SE_Manager.SE_NAME.Shot);
    }

    void LateUpdate()
    {
        //ダメージ処理
        Arts_Process.Damage(homingDamage, damage, true);

        //オブジェクトを消す
        if (transform.childCount == 0) Destroy(gameObject);
    }
}
