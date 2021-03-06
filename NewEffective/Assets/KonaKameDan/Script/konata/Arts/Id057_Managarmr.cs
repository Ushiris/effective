﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id057_Managarmr : MonoBehaviour
{
    [SerializeField] GameObject managarmrParticleObj;
    [SerializeField] float force = 30f;
    [SerializeField] float defaultDamage = 1.2f;

    [Header("射撃のスタック数に応じてたされる数")]
    [SerializeField] float plusDamage = 0.05f;

    GameObject managarmrParticle;
    GameObject target;

    new ParticleSystem particleSystem;
    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        var shotCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Shot);
        var explosionCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Explosion);

        managarmrParticle = Instantiate(managarmrParticleObj, transform);
        particleSystem = managarmrParticle.GetComponent<ParticleSystem>();

        //ダメージ処理
        var particleDamage = Arts_Process.SetParticleDamageProcess(managarmrParticle);
        var effectCount = shotCount + explosionCount;
        var damage = defaultDamage + (plusDamage * (float)effectCount);
        Arts_Process.Damage(particleDamage, artsStatus, damage, true);


        //敵のポジションを持ってくる
        target = Arts_Process.GetNearTarget(artsStatus);

        //SE
        Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id057_Managarmr_first, transform.position, artsStatus);
    }

    // Update is called once per frame
    void Update()
    {
        //ホーミング処理
        if (target != null && managarmrParticle != null)
        {
            Arts_Process.HomingParticle(particleSystem, target, force);
        }

        //オブジェクトを消す

        if (managarmrParticle != null)
        {
            if (managarmrParticle.transform.childCount == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
