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

    //エフェクトの所持数用
    int shotCount = 0;
    int spreadCount = 0;
    float damage;

    GameObject shotGunParticleObj;
    ParticleHit homingDamage;
    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //パーティクル生成
        shotGunParticleObj = Instantiate(shotGunParticle, transform);

        //エフェクトの所持数を代入
        shotCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Shot);
        spreadCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Spread);


        //ダメージ
        homingDamage = Arts_Process.SetParticleDamageProcess(shotGunParticleObj);

        //ダメージの計算
        damage = defaultDamage + (plusDamage * (float)shotCount);

        //弾数を増やす
        var ps = shotGunParticleObj.GetComponent<ParticleSystem>();
        var pse = ps.emission;
        Arts_Process.SetBulletCount(pse, defaultBullet, addBullet, spreadCount);

        //SE
        SE_Manager.SePlay(SE_Manager.SE_NAME.Shot);
    }

    void LateUpdate()
    {
        //ダメージ処理
        Arts_Process.Damage(homingDamage,artsStatus, damage, true);

        //オブジェクトを消す
        if (transform.childCount == 0) Destroy(gameObject);
    }
}
