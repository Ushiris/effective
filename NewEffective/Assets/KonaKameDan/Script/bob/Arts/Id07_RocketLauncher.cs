using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id07_RocketLauncher : MonoBehaviour
{
    [SerializeField] GameObject RocketLauncherParticleObj;
    [SerializeField] float defaultDamage = 2f;
    GameObject RocketLauncherParticle;

    [Header("爆発のスタック数に応じてたされる数")]
    [SerializeField] float plusDamageExplosion = 0.05f;

    [Header("射撃のスタック数に応じてたされる数")]
    [SerializeField] float plusDamageShot = 0.05f;

    //エフェクトの所持数用
    int explosionCount;
    int shotCount;
    float damage;

    ArtsStatus artsStatus;

    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //パーティクル生成
        RocketLauncherParticle = Instantiate(RocketLauncherParticleObj, transform);

        //エフェクトの所持数を代入
        explosionCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Explosion);
        shotCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Shot);

        //ダメージ処理
        Damage();

        //SE
        Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id07_RocketLauncher_first, transform.position, artsStatus);
        Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id07_RocketLauncher_second, transform.position, artsStatus);
    }
    void Update()
    {
        //オブジェクトを消す
        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }

    void Damage()
    {
        //ダメージ
        var rocketLauncherDamage = Arts_Process.SetParticleDamageProcess(RocketLauncherParticle);

        //ダメージの計算
        damage = defaultDamage + ((plusDamageExplosion + plusDamageShot) * (float)explosionCount);

        //ダメージ処理
        Arts_Process.Damage(rocketLauncherDamage, artsStatus, damage, true);
    }
}
