using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id07_RocketLauncher : MonoBehaviour
{
    [SerializeField] GameObject RocketLauncherParticleObj;
    [SerializeField] float defaultDamage = 2f;
    GameObject RocketLauncherParticle;

    [Header("爆発のスタック数に応じてたされる数")]
    [SerializeField] float plusDamage = 0.05f;

    //エフェクトの所持数用
    int explosionCount;
    float damage;

    ArtsStatus artsStatus;

    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //パーティクル生成
        RocketLauncherParticle = Instantiate(RocketLauncherParticleObj, transform);

        //エフェクトの所持数を代入
        explosionCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Explosion);

        //ダメージ処理
        Damage();

        //SE
        SE_Manager.SePlay(SE_Manager.SE_NAME.Id07_RocketLauncher_first);
        SE_Manager.SePlay(SE_Manager.SE_NAME.Id07_RocketLauncher_second);
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
        damage = defaultDamage + (plusDamage * (float)explosionCount);

        //ダメージ処理
        Arts_Process.Damage(rocketLauncherDamage, artsStatus, damage, true);
    }
}
