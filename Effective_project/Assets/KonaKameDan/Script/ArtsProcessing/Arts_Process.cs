using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arts_Process : MonoBehaviour
{
    /// <summary>
    /// 一番近い敵の位置を返す
    /// </summary>
    /// <returns></returns>
    public static GameObject GetEnemyTarget()
    {
        GameObject pl = PlayerManager.GetManager.GetPlObj;
        Vector3 v3 = pl.transform.position;
        return pl.GetComponentInChildren<EnemyFind>().GetNearEnemyPos(v3);
    }

    /// <summary>
    /// ダメージ処理をアタッチする
    /// </summary>
    /// <param name="obj">ダメージ処理を付けたい相手</param>
    public static void SetParticleDamageProcess(GameObject obj)
    {
        obj.AddComponent<ParticleHit>();
    }

    
    public static void Damage(ParticleHit hit, float hitDefaultDamage,bool status, string hitObjTag = "Enemy")
    {
        hit.hitDamageDefault = hitDefaultDamage;
        if (status) hit.plusFormStatus = PlayerManager.GetManager.GetPlObj.GetComponent<Status>().status[Status.Name.STR];
        hit.hitObjTag = hitObjTag;
    }

    /// <summary>
    /// ホーミングの処理
    /// </summary>
    /// <param name="particleSystem"></param>
    /// <param name="force">力</param>
    public static void HomingParticle(ParticleSystem particleSystem, GameObject target, float force)
    {
        ParticleSystem.MainModule particleSystemMainModule;
        particleSystemMainModule = particleSystem.main;

        //パーティクルの最大値
        int maxParticles = particleSystemMainModule.maxParticles;

        //全てのパーティクルを入れる
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[maxParticles];

        //パーティクルを取得
        particleSystem.GetParticles(particles);

        //速度の計算
        float forceDeltaTime = force * Time.deltaTime;

        //ターゲットの座標
        Vector3 targetTransformedPosition = target.transform.position;

        //現在出ているパーティクルの数
        int particleCount = particleSystem.particleCount;

        //パーティクル全ての位置をターゲットに向かわせる
        for (int i = 0; i < particleCount; i++)
        {
            //方向
            Vector3 directionToTarget = Vector3.Normalize(targetTransformedPosition - particles[i].position);
            //速度
            Vector3 seekForce = directionToTarget * forceDeltaTime;
            //パーティクルに代入
            particles[i].velocity += seekForce;
        }

        //パーティクルに反映
        particleSystem.SetParticles(particles, particleCount);
    }
}
